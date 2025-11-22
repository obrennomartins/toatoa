using Microsoft.Extensions.Caching.Memory;
using ToAToa.Domain.Interfaces;

namespace ToAToa.DataAccess.Caching;

public class CacheService(IMemoryCache cache) : ICacheService
{
    private const int DefaultExpirationTimeInHours = 6;
    public Task<T?> GetAsync<T>(string key)
    {
        var value = cache.TryGetValue(key, out T? result)
            ? result
            : default;

        return Task.FromResult(value);
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? expirationTime = null)
    {
        var cacheExpiration = expirationTime ?? TimeSpan.FromHours(DefaultExpirationTimeInHours);
        cache.Set(key, value, cacheExpiration);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key)
    {
        cache.Remove(key);
        return Task.CompletedTask;
    }
}
