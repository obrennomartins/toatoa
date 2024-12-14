using System.Collections;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ToAToa.Presentation.Middlewares;

public class RemoveNulosVaziosMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;

        using var tempBody = new MemoryStream();
        context.Response.Body = tempBody;

        await next(context);

        if (context.Response.ContentType != null && context.Response.ContentType.Contains("application/json"))
        {
            tempBody.Seek(0, SeekOrigin.Begin);

            var bodyText = await new StreamReader(tempBody).ReadToEndAsync();
            if (!string.IsNullOrWhiteSpace(bodyText))
            {
                var obj = JsonSerializer.Deserialize<object>(bodyText, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var cleanedObj = RemoveEmptyProperties(obj);

                var responseJson = JsonSerializer.Serialize(cleanedObj, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    WriteIndented = true
                });

                context.Response.Body = originalBodyStream;
                await context.Response.WriteAsync(responseJson);
            }
        }
        else
        {
            tempBody.Seek(0, SeekOrigin.Begin);
            await tempBody.CopyToAsync(originalBodyStream);
        }
    }

    private object? RemoveEmptyProperties(object? obj)
    {
        if (obj == null)
        {
            return null;
        }
        
        if (obj is JsonElement jsonElement)
        {
            obj = ConvertJsonElement(jsonElement);
        }

        if (obj is IDictionary<string, object> dict)
        {
            var keysToRemove = dict
                .Where(kvp => IsEmpty(kvp.Value))
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var key in keysToRemove)
            {
                dict.Remove(key);
            }

            return dict;
        }

        if (obj is IEnumerable<object> list)
        {
            return list.Where(item => !IsEmpty(item)).ToList();
        }

        var properties = obj?.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(prop => prop is { CanRead: true, CanWrite: true })
            .ToList();

        foreach (var property in properties ?? [])
        {
            var value = property.GetValue(obj);

            if (value == null || IsEmpty(value))
            {
                property.SetValue(obj, null);
            }
            else
            {
                var cleanedValue = RemoveEmptyProperties(value);
                property.SetValue(obj, cleanedValue);
            }
        }

        return obj;
    }

    private bool IsEmpty(object? value)
    {
        if (value == null)
        {
            return true;
        }

        if (value is string str && string.IsNullOrWhiteSpace(str))
        {
            return true;
        }

        if (value is IEnumerable enumerable && !enumerable.Cast<object>().Any())
        {
            return true;
        }

        return false;
    }

    private object? ConvertJsonElement(JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                return element.EnumerateObject().ToDictionary(p => p.Name, p => ConvertJsonElement(p.Value));
            case JsonValueKind.Array:
                return element.EnumerateArray().Select(ConvertJsonElement).ToList();
            case JsonValueKind.String:
                return element.GetString();
            case JsonValueKind.Number:
                if (element.TryGetInt64(out var l))
                {
                    return l;
                }
                return element.GetDouble();
            case JsonValueKind.True:
            case JsonValueKind.False:
                return element.GetBoolean();
            case JsonValueKind.Null:
                return null;
            case JsonValueKind.Undefined:
            default:
                return element.GetRawText();
        }
    }
}
