using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseXY
{
    public static class DictionaryExtensions
    {
      public static bool RemoveValueFromList<TKey>(this Dictionary<TKey, List<Keys?>> dict, Keys k)
      {
         var keysToRemove = new List<TKey>();
         var removed = false;

         foreach (var kvp in dict)
         {
            if (kvp.Value.Remove(k))
            {
               removed = true;
               if (kvp.Value.Count == 0)
               {
                  keysToRemove.Add(kvp.Key);
               }
            }
         }

         foreach (var key in keysToRemove)
         {
            dict.Remove(key);
         }

         return removed;
      }

   }
}
