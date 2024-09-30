using MediatR;
using Microsoft.OpenApi.Models;
using ToAToa.Application.Atividades.Queries.ObterAtividadeAleatoria;

namespace ToAToa.Presentation.Endpoints;

public static class AtividadeEndpoint
{
    public static void RegisterAtividadeEndpoints(this IEndpointRouteBuilder app)
    {
        var atividadeEndpoint = app
            .MapGroup("/api/v1/atividades")
            .WithTags("Atividades")
            .WithOpenApi();
        
        atividadeEndpoint.MapGet("/aleatoria", async (
            IMediator mediator) =>
        {
            var query = new ObterAtividadeAleatoriaQuery();
            var result = await mediator.Send(query);
            return Results.Ok(result);
        }).WithOpenApi(operation => new OpenApiOperation(operation)
        {
            Summary = "Obtém uma atividade aleatória"
        });
    }
}
