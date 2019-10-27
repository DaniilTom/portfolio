using System;

namespace SightMap.BLL.CustomCache
{
    public static class CacheConstants
    {
        public const int DefaultSize = 10;
        public const int DefaultSlidingExpirationTime = 10;
        public const int DefaultAbsoluteExpirationTime = 20;

        public const string Sights = "Sights";
        public const string Reviews = "Reviews";
        public const string SightTypes = "SightTypes";
    }
}
