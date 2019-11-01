using System;
using System.Runtime.Caching;
using System.Threading;

namespace SightMap.BLL.CustomCache
{
    public class CustomCache : ICustomCache
    {
        private static readonly MemoryCache _cache = MemoryCache.Default;
        private static readonly ManualResetEvent mre = new ManualResetEvent(true);
        private static object lockObj = new object();

        public T GetOrAdd<T>(Func<T> func, string key, bool IsSliding = false)
        {
            T result = (T)_cache.Get(key);

            if (result != null)
            {
                return result;
            }

            mre.WaitOne();
            mre.Reset();

            lock (lockObj)
            {
                result = (T)_cache.Get(key);

                if (result != null)
                {
                    mre.Set();
                    return result;
                }

                result = func();

                if (result == null)
                {
                    mre.Set();
                    return result;
                }

                CacheItemPolicy policy = new CacheItemPolicy();
                if (IsSliding)
                {
                    policy.SlidingExpiration = TimeSpan.FromSeconds(CacheConstants.DefaultSlidingExpirationTime);
                    //policy.UpdateCallback = arg => { UpdateCallback(arg, func); };
                    policy.RemovedCallback = arg => { RemoveCallback(arg, func); };
                }
                else
                    policy.AbsoluteExpiration = DateTime.Now.AddSeconds(CacheConstants.DefaultAbsoluteExpirationTime);


                _cache.Set(key, result, policy);

                mre.Set();
            }

            return result;
        }

        public void UpdateCallback<T>(CacheEntryUpdateArguments arg, Func<T> func, int count = 0)
        {
            T result = func();

            _cache.Remove(arg.Key);

            CacheItemPolicy policy = new CacheItemPolicy();
            policy.SlidingExpiration = TimeSpan.FromSeconds(CacheConstants.DefaultSlidingExpirationTime);
            
            
            policy.UpdateCallback = arg => { UpdateCallback(arg, func, count); };

            //arg.
            _cache.Set(arg.Key, result, policy);
        }

        public void RemoveCallback<T>(CacheEntryRemovedArguments arg, Func<T> func, int count = 0)
        {
            T result = func();

            CacheItemPolicy policy = new CacheItemPolicy();
            policy.SlidingExpiration = TimeSpan.FromSeconds(CacheConstants.DefaultSlidingExpirationTime);

            if (count++ < 3)
                policy.RemovedCallback = arg => { RemoveCallback(arg, func, count); };

            _cache.Set(arg.CacheItem.Key, result, policy);
        }
    }
}
