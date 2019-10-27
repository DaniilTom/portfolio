using Microsoft.Extensions.Caching.Memory;
using SightMap.BLL.DTO;
using SightMap.DAL.Models;
using System.Collections.Generic;

namespace SightMap.BLL.CustomCache
{
    public class CustomCache<TFullDto> : ICustomCache<TFullDto> where TFullDto : BaseDTO
    {
        protected IMemoryCache _cache;

        public CustomCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool TryGetCachedValue(string key, out IEnumerable<TFullDto> result)
        {
            return _cache.TryGetValue<IEnumerable<TFullDto>>(key, out result);
        }

        public virtual void SetValueToCache(string key, IEnumerable<TFullDto> value, MemoryCacheEntryOptions options = null)
        {
            if (options is null)
                _cache.Set<IEnumerable<TFullDto>>(key, value);
            else
                _cache.Set<IEnumerable<TFullDto>>(key, value, options);
        }
    }
}
