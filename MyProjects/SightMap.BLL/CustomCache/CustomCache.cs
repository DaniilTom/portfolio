using Microsoft.Extensions.Caching.Memory;
using SightMap.BLL.DTO;
using SightMap.DAL.Models;
using System;
using System.Collections.Generic;

namespace SightMap.BLL.CustomCache
{
    public abstract class CustomCache<TFullDto> : ICustomCache<TFullDto> where TFullDto : BaseDTO
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

    public interface ICustomCache<TFullDto> where TFullDto : BaseDTO
    {
        bool TryGetCachedValue(string key, out IEnumerable<TFullDto> result);
        void SetValueToCache(string key, IEnumerable<TFullDto> value, MemoryCacheEntryOptions options = null);
    }

    public class SlidingCustomCache<TFullDto> : CustomCache<TFullDto> where TFullDto : BaseDTO
    {
        public SlidingCustomCache(IMemoryCache _cache) : base(_cache) { }

        public override void SetValueToCache(string key, IEnumerable<TFullDto> value, MemoryCacheEntryOptions options = null)
        {
            var newOptions = new MemoryCacheEntryOptions();
            newOptions.SetSlidingExpiration(TimeSpan.FromSeconds(CacheConstants.DefaultExpirationTime));
            base.SetValueToCache(key, value, newOptions);
        }
    }

    public class NoSlidingCustomCache<TFullDto> : CustomCache<TFullDto> where TFullDto : BaseDTO
    {
        public NoSlidingCustomCache(IMemoryCache _cache) : base(_cache) { }

        public override void SetValueToCache(string key, IEnumerable<TFullDto> value, MemoryCacheEntryOptions options = null)
        {
            var newOptions = new MemoryCacheEntryOptions();
            newOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheConstants.DefaultExpirationTime));
            base.SetValueToCache(key, value, newOptions);
        }
    }
}
