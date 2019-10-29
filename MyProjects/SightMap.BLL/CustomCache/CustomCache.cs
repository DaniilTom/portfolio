using System;
using System.Runtime.Caching;
using System.Threading;

namespace SightMap.BLL.CustomCache
{
    public class CustomCache : ICustomCache

    {
        private static MemoryCache _cache;
        private static object lockObj;

        public CustomCache()
        {
            _cache = new MemoryCache("CustomCache");
        }

        public T GetOrAdd<T>(Func<T> func, string key, bool IsSliding = false)
        {
            Monitor.Enter(lockObj);

            T result = (T)_cache.Get(key);

            if (result != null)
            {
                Monitor.Exit(lockObj);
                return result;
            }

            result = func();

            if (result == null)
            {
                Monitor.Exit(lockObj);
                return result;
            }

            CacheItemPolicy policy = new CacheItemPolicy();
            if (IsSliding)
            {
                policy.SlidingExpiration = TimeSpan.FromSeconds(CacheConstants.DefaultSlidingExpirationTime);
                policy.UpdateCallback = arg => { UpdateCallback(arg, func); };
            }
            else
                policy.AbsoluteExpiration = DateTime.Now.AddSeconds(CacheConstants.DefaultAbsoluteExpirationTime);


            _cache.Set(key, result, policy);

            Monitor.Exit(lockObj);

            return result;
        }

        public static void UpdateCallback<T>(CacheEntryUpdateArguments arg, Func<T> func)
        {
            Monitor.Enter(lockObj);

            T result = (T)_cache.Get(arg.Key);

            if(result == null)
            {
                result = func();

                CacheItemPolicy policy = arg.UpdatedCacheItemPolicy;

                _cache.Set(arg.Key, result, policy);
            }

            Monitor.Exit(lockObj);
        }
    }
}
