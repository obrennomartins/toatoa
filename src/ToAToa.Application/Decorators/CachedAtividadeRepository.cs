using ToAToa.Domain.Entities;
using ToAToa.Domain.Interfaces;

namespace ToAToa.Application.Decorators;

public class CachedAtividadeRepository(
    IAtividadeRepository innerRepository,
    ICacheService cache) : IAtividadeRepository
{
    private const string TotalAtividadesCacheKey = "TotalAtividades";
    private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(6);

    public async Task<Atividade?> ObterAtividadeAleatoriaAsync()
    {
        var totalAtividades = await ObterTotalAtividadesCachedAsync();

        if (totalAtividades == 0)
        {
            return null;
        }

        var random = new Random();
        var skip = random.Next(totalAtividades);

        return await innerRepository.ObterAtividadePorSkipAsync(skip);
    }

    public async Task<int> ObterTotalAtividadesAsync() =>
        await innerRepository.ObterTotalAtividadesAsync();

    public async Task<Atividade?> ObterAtividadePorSkipAsync(int skip) =>
        await innerRepository.ObterAtividadePorSkipAsync(skip);

    public async Task InvalidarCacheAsync() =>
        await cache.RemoveAsync(TotalAtividadesCacheKey);

    private async Task<int> ObterTotalAtividadesCachedAsync()
    {
        var cachedTotal = await cache.GetAsync<int?>(TotalAtividadesCacheKey);

        if (cachedTotal.HasValue)
        {
            return cachedTotal.Value;
        }

        var total = await innerRepository.ObterTotalAtividadesAsync();
        await cache.SetAsync(TotalAtividadesCacheKey, total, CacheDuration);

        return total;
    }
}