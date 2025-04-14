using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
//using Microsoft.Extensions.Caching.Memory;

namespace Cache.CacheApi
{
    public class SCache
    {

        public static List<string> GetListName()
        {
            List<string> list = new List<string>();

            foreach (var item in MemoryCache.Default)
            {
                //add the item.keys to list
                list.Add(item.Key);
            }
            return list;
        }

        public static bool Exist(string name)
        {
            ObjectCache cache = MemoryCache.Default;
            return cache.Contains(name);
        }

        public static object GetDataCache(string name)
        {
            ObjectCache cache = MemoryCache.Default;

            object fileContents = cache[name];
            return fileContents;
        }
        public static void SetDataCache(string name, object data)
        {
            ObjectCache cache = MemoryCache.Default;
            if (data == null)
            {
                var dataCache = cache[name];
                if (dataCache != null)
                    cache.Remove(name);
                return;
            }
            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(2) };
            cache.Set(name, data, policy);
        }
        public static void SetDataCache(string name, object data, int minExpire)
        {
            ObjectCache cache = MemoryCache.Default;

            if (data == null)
            {
                var dataCach = cache[name];
                if (dataCach != null)
                    cache.Remove(name);

                return;
            }

            var policy =
                new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(minExpire) };

            cache.Set(name, data, policy);
        }
        public static void ClearAll()
        {

            ObjectCache cache = MemoryCache.Default;
            var list = cache.Select(a => a.Key);
            foreach (var item in list)
            {
                cache.Remove(item);
            }
        }


        public static void ClearPreKey(string preKey)
        {
            ObjectCache cache = MemoryCache.Default;
            var list = cache.Select(a => a.Key);
            foreach (var item in list.Where(a => a.Contains(preKey)))
            {
                cache.Remove(item);
            }
        }

        public static void Clear(string Key)
        {
            ObjectCache cache = MemoryCache.Default;
            var list = cache.Select(a => a.Key);
            foreach (var item in list.Where(a => a == Key))
            {
                cache.Remove(item);
            }
        }




    }
}
