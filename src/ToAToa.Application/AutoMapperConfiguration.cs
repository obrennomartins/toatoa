using AutoMapper;
using ToAToa.Application.Atividades.Queries.ObterAtividadeAleatoria;
using ToAToa.Domain.Entities;

namespace ToAToa.Application;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<Atividade, ObterAtividadeAleatoriaResponse>();
    }
}
