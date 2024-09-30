using AutoMapper;
using ToAToa.Application.Abstractions.Messaging;
using ToAToa.Domain.Abstractions;
using ToAToa.Domain.Interfaces;

namespace ToAToa.Application.Atividades.Queries.ObterAtividadeAleatoria;

public class ObterAtividadeAleatoriaQueryHandler(
    IAtividadeRepository atividadeRepository,
    IMapper mapper
    ) : IQueryHandler<ObterAtividadeAleatoriaQuery, Result<ObterAtividadeAleatoriaResponse>>
{
    public async Task<Result<ObterAtividadeAleatoriaResponse>> Handle(ObterAtividadeAleatoriaQuery request, CancellationToken cancellationToken)
    {
        var atividade = await atividadeRepository.ObterAtividadeAleatoriaAsync();
        var atividadeResponse = mapper.Map<ObterAtividadeAleatoriaResponse>(atividade);
        var result = Result.Create(atividadeResponse);

        return result;
    }
}
