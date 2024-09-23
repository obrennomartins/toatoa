using Microsoft.OpenApi.Models;
using ToAToa.Domain.Interfaces;

namespace ToAToa.Presentation.Endpoints;

public static class AtividadeEndpoint
{
    public static void RegisterAtividadeEndpoints(this IEndpointRouteBuilder app)
    {
        var atividadeEndpoint = app
            .MapGroup("/api/v1/atividades")
            .WithTags("Atividades")
            .WithOpenApi();
        
        atividadeEndpoint.MapGet("/aleatoria", async (IAtividadeRepository repository) =>
        {
            var atividade = await repository.ObterAtividadeAleatoriaAsync();
            return TypedResults.Ok(atividade?.Descricao);
        }).WithOpenApi(operation => new OpenApiOperation(operation)
        {
            Summary = "Obtém uma atividade aleatória"
        });
    }
}
