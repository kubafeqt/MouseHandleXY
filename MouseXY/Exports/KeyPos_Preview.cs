using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseXY
{
    class KeyPos_Preview
    {

      public static List<KeyPos_Preview> KeyPositionsList = new();
      public static Dictionary<int, string> SetNamesDict = new();

      public static string showedSetName = "default";
      public static string selectedFileName;

      public string Key { get; set; }
      public Point Position { get; set; }
      public string SetName { get; set; }
      public DateTime CreatedAt { get; set; }
      public bool IsActive { get; set; }

      public KeyPos_Preview(string key, Point position, string setName, DateTime createdAt, bool isActive)
      {
         Key = key;
         Position = position;
         SetName = setName;
         CreatedAt = createdAt;
         IsActive = isActive;
         KeyPositionsList.Add(this);
      }

      public static void ClearData()
      {
         KeyPositionsList.Clear();
         SetNamesDict.Clear();
      }

   }
}
