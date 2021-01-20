using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAgenda.Helpers
{
    public class DataCache
    {
        internal class CacheItem
        {
            public string S { get; set; }
            public DateTime Stamp { get; set; }
            public DateTime Expires { get; set; }
        }

        private static DataCache instance;
        private Dictionary<string, CacheItem> data;

        private DataCache()
        {
            data = new Dictionary<string, CacheItem>();
        }

        public void Remove(string key)
        {
            if (!data.ContainsKey(key))
                return;

            data.Remove(key);
        }

        public void Store(string key, string value, int maxAgeSeconds)
        {
            data[key] = new CacheItem
            {
                S = value,
                Stamp = DateTime.Now,
                Expires = DateTime.Now.AddSeconds(maxAgeSeconds)
            };
        }

        public string Retrieve(string key)
        {
            if (!data.ContainsKey(key))
                return null;

            if (data[key].Expires < DateTime.Now)
                data.Remove(key);

            return data.ContainsKey(key) ? data[key].S : null;
        }

        public static DataCache Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataCache();
                }

                return instance;
            }
        }

    }
}
