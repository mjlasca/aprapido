using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Runtime.Caching;

namespace ProyectoBrokerDelPuerto
{
    public class CacheManager
    {
        private static readonly MemoryCache Cache = MemoryCache.Default;

        public static void AddToCache(string key, object value, TimeSpan expiration)
        {
            Cache.Add(key, value, DateTimeOffset.Now.Add(expiration));
        }

        public static IEnumerable<KeyValuePair<string, object>> GetAllCacheItems()
        {
            foreach (var item in Cache)
            {
                yield return item;
            }
        }

        public static object GetFromCache(string key)
        {
            return Cache.Get(key);
        }

        public static void RemoveFromCache(string key)
        {
            Cache.Remove(key);
        }

        public static void RemoveFromCachePref(string pref)
        {
            foreach (var item in GetAllCacheItems())
            {
                if(item.Key.IndexOf(pref) != -1)
                {
                    Cache.Remove(item.Key);
                }
            }
        }
    }


}
