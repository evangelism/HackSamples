using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleWeb.Models
{
    public class DeviceFamilyInfo
    {
        public string Family { get; set; }
        public int Count { get; set; } = 1;
    }

    public static class DeviceFamilyStore
    {
        private static Dictionary<string, int> store = new Dictionary<string, int>();
        public static IEnumerable<DeviceFamilyInfo> Get()
        {
            var L = new List<DeviceFamilyInfo>();
            foreach(var x in store.Keys)
            {
                L.Add(new DeviceFamilyInfo() { Family = x, Count = store[x] });
            }
            return L;
        }

        public static void Register(string f)
        {
            if (store.ContainsKey(f)) store[f]++;
            else store.Add(f, 1);
        }
    }
}