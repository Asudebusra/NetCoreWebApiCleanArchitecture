using App.Application.Contracts.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace App.Caching
{
    public class CacheService(IMemoryCache memoryCache) : ICacheService//.net8 ile gelen primary constructor'ı kulladık.Distributed cache i alıp redise de gönderebilirdik.
    {
        public Task AddAsync<T>(string cacheKey, T value, TimeSpan exprTimeSpan)
        {
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = exprTimeSpan//şuanki zamandan itibaren exprTimeSpan kadar geçerli olacak
            };

            memoryCache.Set(cacheKey, value, cacheOptions);

            return Task.CompletedTask;
        }

        public Task<T?> GetAsync<T>(string cacheKey)
        {
            if(memoryCache.TryGetValue(cacheKey,out T cacheItem)) return Task.FromResult(cacheItem);

            return Task.FromResult<T?>(default(T));

        }

        public Task RemoveAsync<T>(string cacheKey)
        {
           memoryCache.Remove(cacheKey);

           return Task.CompletedTask;
        }
    }
}
