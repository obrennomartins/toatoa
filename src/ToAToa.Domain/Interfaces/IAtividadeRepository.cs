using ToAToa.Domain.Entities;

namespace ToAToa.Domain.Interfaces;

public interface IAtividadeRepository
{
    /// <summary>
    /// Obtém uma Atividade aleatória
    /// </summary>
    /// <returns>Uma Atividade</returns>
    Task<Atividade?> ObterAtividadeAleatoriaAsync();
}
