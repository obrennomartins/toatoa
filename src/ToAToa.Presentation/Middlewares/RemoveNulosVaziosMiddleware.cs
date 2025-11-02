using System.Collections;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ToAToa.Presentation.Middlewares;

public class RemoveNulosVaziosMiddleware(RequestDelegate next)
{
    private static readonly JsonSerializerOptions DeserializeOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private static readonly JsonSerializerOptions SerializeOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true
    };

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
                var obj = JsonSerializer.Deserialize<object>(bodyText, DeserializeOptions);

                var cleanedObj = RemoveEmptyProperties(obj);

                var responseJson = JsonSerializer.Serialize(cleanedObj, SerializeOptions);

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

    private static object? RemoveEmptyProperties(object? obj)
    {
        switch (obj)
        {
            case null:
                return null;

            case JsonElement jsonElement:
            {
                obj = ConvertJsonElement(jsonElement);
                if (obj == null)
                {
                    return null;
                }
                break;
            }
        }

        switch (obj)
        {
            case IDictionary<string, object> dict:
            {
                var keysToRemove = dict
                    .Where(kvp => IsEmpty(kvp.Value))
                    .Select(kvp => kvp.Key)
                    .ToList();

                foreach (var key in keysToRemove)
                {
                    dict.Remove(key);
                }

                foreach (var key in dict.Keys.ToList())
                {
                    dict[key] = RemoveEmptyProperties(dict[key])!;
                }

                return dict;
            }

            case IEnumerable<object> list:
                return list
                    .Select(RemoveEmptyProperties)
                    .Where(item => !IsEmpty(item))
                    .ToList();
        }

        var properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(prop => prop is { CanRead: true, CanWrite: true })
            .ToList();

        foreach (var property in properties)
        {
            var value = property.GetValue(obj);

            if (IsEmpty(value))
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
    private static bool IsEmpty(object? value) =>
        value switch
        {
            null => true,
            string str when string.IsNullOrWhiteSpace(str) => true,
            IEnumerable enumerable and not string => !enumerable.Cast<object>().Any(),
            _ => false
        };

    private static object? ConvertJsonElement(JsonElement element) =>
        element.ValueKind switch
        {
            JsonValueKind.Object => element.EnumerateObject().ToDictionary(p => p.Name, p => ConvertJsonElement(p.Value)),
            JsonValueKind.Array => element.EnumerateArray().Select(ConvertJsonElement).ToList(),
            JsonValueKind.String => element.GetString(),
            JsonValueKind.Number => element.TryGetInt64(out var l) ? l : element.GetDouble(),
            JsonValueKind.True or JsonValueKind.False => element.GetBoolean(),
            JsonValueKind.Null => null,
            _ => element.GetRawText()
        };
}
