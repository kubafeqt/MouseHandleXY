using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseXY
{
   class KeyPos
   {

      public static List<KeyPos> KeyPositions = new();
      public static Dictionary<Keys, Point> keysPosition = new(); //stores the position of the mouse for each key

      public string Key { get; set; }
      public Point Position { get; set; }
      public string SetName { get; set; }
      public DateTime CreatedAt { get; set; }
      public bool IsActive { get; set; }

      public KeyPos(string key, Point position, string setName, DateTime createdAt, bool isActive)
      {
         Key = key;
         Position = position;
         SetName = setName;
         CreatedAt = createdAt;
         IsActive = isActive;
         if (!KeyPositions.Any(k => k.Key == key))
         {
            KeyPositions.Add(this);
         }
         else
         {
            var existingKeyPos = KeyPositions.Find(k => k.Key == key);
            if (existingKeyPos != null)
            {
               existingKeyPos.Position = position;
               existingKeyPos.SetName = setName;
               existingKeyPos.CreatedAt = createdAt;
               existingKeyPos.IsActive = isActive;
            }
         }
      }

   }
}
