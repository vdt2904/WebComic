using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using WebComic.Models;

namespace WebComic.Helpter
{
    public class Reuse
    {
        private readonly IDistributedCache _distributedCache;
        public Reuse(IDistributedCache distributedCache) { 
            _distributedCache = distributedCache;
        }
        public async Task ReuseCURD<T>(T collection, string name)
        {

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            };
            
            await _distributedCache.SetStringAsync(name, JsonConvert.SerializeObject(collection), cacheOptions);
        }
    }
}

