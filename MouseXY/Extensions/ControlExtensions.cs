using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseXY
{
    public static class ControlExtensions
    {
      public static IEnumerable<T> OfTag<T>(this IEnumerable<T> controls, string tagValue) where T : Control
      {
         return controls.Where(c =>
             c.Tag is string tag &&
             string.Equals(tag, tagValue, StringComparison.OrdinalIgnoreCase));
      }
   }
}
