using Microsoft.Extensions.Caching.Memory;
using SightMap.BLL.DTO;
using System.Collections.Generic;

namespace SightMap.BLL.CustomCache
{
    public interface ICustomCache<TFullDto> where TFullDto : BaseDTO
    {
        bool TryGetCachedValue(string key, out IEnumerable<TFullDto> result);
        void SetValueToCache(string key, IEnumerable<TFullDto> value, MemoryCacheEntryOptions options = null);
    }
}
