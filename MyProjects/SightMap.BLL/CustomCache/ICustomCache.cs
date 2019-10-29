using Microsoft.Extensions.Caching.Memory;
using SightMap.BLL.DTO;
using System;
using System.Collections.Generic;

namespace SightMap.BLL.CustomCache
{
    public interface ICustomCache
    {
        T GetOrAdd<T>(Func<T> func, string key, bool IsSliding);
    }
}
