using ToAToa.Domain.Entities;

namespace ToAToa.Domain.Interfaces;

public interface IAtividadeRepository
{
    /// <summary>
    /// Obtém uma Atividade aleatória
    /// </summary>
    /// <returns>Uma <see cref="Atividade"/></returns>
    Task<Atividade?> ObterAtividadeAleatoriaAsync();

    /// <summary>
    /// Obtém o total de atividades
    /// </summary>
    /// <returns>Número total de atividades</returns>
    Task<int> ObterTotalAtividadesAsync();

    /// <summary>
    /// Obtém uma atividade pulando um número específico de registros
    /// </summary>
    /// <param name="skip">Número de registros a pular</param>
    /// <returns>Uma <see cref="Atividade"/></returns>
    Task<Atividade?> ObterAtividadePorSkipAsync(int skip);
}
