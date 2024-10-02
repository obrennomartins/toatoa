using System.Text.Json;
using System.Text.Json.Nodes;

namespace ToAToa.Presentation.Middlewares;

/// <summary>
/// Remove os atributos nulos e vazios da resposta JSON.
/// </summary>
public class RemoveVaziosJsonMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;

        using var newBodyStream = new MemoryStream();
        context.Response.Body = newBodyStream;

        await next(context);

        context.Response.Body = originalBodyStream;
        newBodyStream.Seek(0, SeekOrigin.Begin);

        var responseBody = await new StreamReader(newBodyStream).ReadToEndAsync();
        var cleanedResponseBody = RemoveEmptyValues(responseBody) ?? "";

        await context.Response.WriteAsync(cleanedResponseBody);
    }

    private static string? RemoveEmptyValues(string responseBody)
    {
        var jsonObject = JsonNode.Parse(responseBody);
        CleanJsonNode(jsonObject);
        return jsonObject?.ToJsonString(new JsonSerializerOptions { WriteIndented = true });
    }

    private static void CleanJsonNode(JsonNode? node)
    {
        switch (node)
        {
            case JsonObject jsonObject:
            {
                var propertiesToRemove = new List<string>();

                foreach (var prop in jsonObject)
                {
                    CleanJsonNode(prop.Value);

                    if (ShouldRemoveProperty(prop.Value))
                    {
                        propertiesToRemove.Add(prop.Key);
                    }
                }

                foreach (var propKey in propertiesToRemove)
                {
                    jsonObject.Remove(propKey);
                }

                break;
            }
            case JsonArray jsonArray:
            {
                for (var i = jsonArray.Count - 1; i >= 0; i--)
                {
                    CleanJsonNode(jsonArray[i]);
                    if (ShouldRemoveProperty(jsonArray[i]))
                    {
                        jsonArray.RemoveAt(i);
                    }
                }

                break;
            }
        }
    }

    private static bool ShouldRemoveProperty(JsonNode? node)
    {
        return node is null ||
               (node is JsonValue jsonValue && string.IsNullOrEmpty(jsonValue.ToString())) ||
                node is JsonObject { Count: 0 } ||
               node is JsonArray { Count: 0 };
    }
}
