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
      public static Dictionary<int, string> setNames = new();

      //Setname: TODO ->
      // uložit/načíst z db
      //  
      // 
      // 
      public static int PossibleFreeIdForSetname()
      {
         var usedIds = setNames.Keys.OrderBy(id => id).ToList();
         int expectedId = 1;
         foreach (var id in usedIds)
         {
            if (id == expectedId)
               expectedId++;
            else if (id > expectedId)
               break; // expectedId je volné
         }
         return expectedId;
      }

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
