using ToAToa.Application.Abstractions.Messaging;
using ToAToa.Domain.Abstractions;

namespace ToAToa.Application.Atividades.Queries.ObterAtividadeAleatoria;

public record ObterAtividadeAleatoriaQuery : IQuery<Result<ObterAtividadeAleatoriaResponse>>;
