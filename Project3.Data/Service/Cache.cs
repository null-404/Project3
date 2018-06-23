using Microsoft.Extensions.Caching.Memory;
using Project3.Data.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project3.Data.Service
{
    public class Cache : ICache
    {
        protected IMemoryCache memoryCache;

        public Cache(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public void Set(string name, object value)
        {
            object cacheEntry;
            if (memoryCache.TryGetValue(name, out cacheEntry))
            {
                Remove(name);
            }
            memoryCache.Set(name, value);
        }

        public void Remove(string name)
        {

            memoryCache.Remove(name);

        }

        public object Get(string name)
        {
            object cacheEntry;
            memoryCache.TryGetValue(name, out cacheEntry);

            return cacheEntry;
        }

        public void Set(string name, object value, long outtime)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
           .SetAbsoluteExpiration(TimeSpan.FromSeconds(outtime));
            memoryCache.Set(name, value, cacheEntryOptions);
        }
    }
}
