using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SightMap.BLL.DTO;
using SightMap.BLL.Infrastructure.Implementations;
using SightMap.BLL.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SightMap
{
    public static class CacheConst
    {
        public const int DefaultSize = 10;
        public const int DefaultExpirationTime = 10;

        public const string Sights = "Sights";
        public const string Reviews = "Reviews";
        public const string SightTypes = "SightTypes";

        public static void PostEvictionCallbackMethod(object key, object value, EvictionReason reason, object state)
        {
            //switch(value)
            //{
            //    case IEnumerable<SightDTO> en:
            //        break;
            //    default: throw new Exception("PostEvictionCallbackMethod");
            //}

            string stringKey = (string)key;

            if (stringKey.Equals(Sights, StringComparison.InvariantCultureIgnoreCase))
            {
                //new DataLoader<SightDTO, SightFilterDTO>().SetDataToCache(stringKey);
            }
            else if (stringKey.Equals(SightTypes, StringComparison.InvariantCultureIgnoreCase))
            {

            }
            else if (stringKey.Equals(Reviews, StringComparison.InvariantCultureIgnoreCase))
            {

            }
        }

        //public class DataLoader<TFullDto, TFilterDto>
        //    where TFullDto : BaseDTO
        //    where TFilterDto : BaseFilterDTO, new()
        //{
        //    private IBaseManager<TFullDto, TFilterDto> _manager;
        //    private IMemoryCache _cache;

        //    public DataLoader(IBaseManager<TFullDto, TFilterDto> manager, IMemoryCache cache)
        //    {
        //        _manager = manager;
        //    }

        //    public void SetDataToCache(string key)
        //    {
        //        var resultObject = _manager.GetListObjects(new TFilterDto { Size = DefaultSize });

        //        var memCacheOptions = new MemoryCacheEntryOptions();

        //        memCacheOptions.RegisterPostEvictionCallback(CacheManager.PostEvictionCallbackMethod);
        //        memCacheOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(CacheManager.DefaultExpirationTime));

        //        _cache.Set<IEnumerable<TFullDto>>(key, resultObject, memCacheOptions);
        //    }
        //}
    }
}
