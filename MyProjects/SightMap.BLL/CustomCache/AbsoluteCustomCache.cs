using Microsoft.Extensions.Caching.Memory;
using SightMap.BLL.DTO;
using System;
using System.Collections.Generic;

namespace SightMap.BLL.CustomCache
{
    public class AbsoluteCustomCache<TFullDto> : CustomCache<TFullDto> where TFullDto : BaseDTO
    {
        public AbsoluteCustomCache(IMemoryCache _cache) : base(_cache) { }

        public override void SetValueToCache(string key, IEnumerable<TFullDto> value, MemoryCacheEntryOptions options = null)
        {
            if (options is null)
                options = new MemoryCacheEntryOptions();

            options.SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheConstants.DefaultAbsoluteExpirationTime));
            base.SetValueToCache(key, value, options);
        }
    }
}
