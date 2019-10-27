using Microsoft.Extensions.Caching.Memory;
using SightMap.BLL.DTO;
using System;
using System.Collections.Generic;

namespace SightMap.BLL.CustomCache
{
    public class SlidingCustomCache<TFullDto> : AbsoluteCustomCache<TFullDto> where TFullDto : BaseDTO
    {
        public SlidingCustomCache(IMemoryCache _cache) : base(_cache) { }

        public override void SetValueToCache(string key, IEnumerable<TFullDto> value, MemoryCacheEntryOptions options = null)
        {
            if (options is null)
                options = new MemoryCacheEntryOptions();

            options.SetSlidingExpiration(TimeSpan.FromSeconds(CacheConstants.DefaultSlidingExpirationTime));
            base.SetValueToCache(key, value, options);
        }
    }
}
