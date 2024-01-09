using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace categories_api.CacheService
{
    public class RedisCacheService
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public T? GetCashedData<T>(string key)
        {
            var jsonData = _distributedCache.Get(key);
            if (jsonData is null)
                return default(T);

            return JsonSerializer.Deserialize<T>(jsonData);
        }

        public void AddCashedData<T>(string key, T data, TimeSpan duration)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = duration
            };

            var jsonData = JsonSerializer.Serialize(data);
            _distributedCache.SetString(key, jsonData, options);
        }
    }
}
