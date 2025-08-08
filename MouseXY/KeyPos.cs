using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MouseXY
{
   class KeyPos
   {    
      public static List<KeyPos> KeyPositions = new(); //pro zobrazování dat
      public static Dictionary<Keys, Point> keysPositionDict { get; private set; } = new(); //pro manipulaci s klávesy - stores the position of the mouse for each key
      public static Dictionary<int, string> setNames = new();
      //public static List<string> setNames = new();
      public static string selectedSetName = "default"; // pro manipulaci s klávesy - stores the name of the set of keys
      public static string showedSetName = "default"; // pro zobrazení v UI, aby se neukazoval default setName

      public static int PossibleFreeIdInDictKeys(Dictionary<int, string> dict)
      {
         var usedIds = dict.Keys.OrderBy(id => id).ToList();
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
         InitializeKeyPositions();
      }

      private void InitializeKeyPositions()
      {
         if (!KeyPositions.Any(k => k.Key == Key && k.SetName == SetName)) //key in setame does not exist - add new
         {
            KeyPositions.Add(this);
            if (SetName == selectedSetName)
            {
               keysPositionDict.Add((Keys)Enum.Parse(typeof(Keys), Key), Position);
            }
         }
         else //key in setName exists - edit
         {
            var existingKeyPos = KeyPositions.Find(k => k.Key == Key && k.SetName == SetName);
            if (existingKeyPos != null)
            {
               existingKeyPos.Position = Position;
               existingKeyPos.SetName = SetName;
               existingKeyPos.CreatedAt = CreatedAt;
               existingKeyPos.IsActive = IsActive;
               if (SetName == selectedSetName)
               {
                  keysPositionDict[(Keys)Enum.Parse(typeof(Keys), Key)] = Position; // update the position in the dictionary
               }
            }
         }
      }

      public static void CreateUpdateKeyPosition(string key, Point position, bool selectedSetname = true) // updates the position of the key in the selected set
      {
         string setName = selectedSetname ? selectedSetName : showedSetName; //determine if we are updating the selected set or the displayed set
         var existingKeyPos = KeyPositions.Find(k => k.Key == key && k.SetName == setName);
         if (existingKeyPos != null)
         {
            existingKeyPos.Position = position;
            if (selectedSetname)
            {
               keysPositionDict[(Keys)Enum.Parse(typeof(Keys), key)] = position; // update the position in the dictionary
            }
         }
         else
         {
            new KeyPos(key, position, showedSetName, DateTime.Now, true);
         }
      }

      public static void UpdateKeyPosDict(Keys? key = null)
      {
         if (key == null)
         {
            keysPositionDict.Clear();
            foreach (var keyPos in KeyPositions.Where(k => k.SetName == selectedSetName && k.IsActive))
            {
               keysPositionDict[(Keys)Enum.Parse(typeof(Keys), keyPos.Key)] = keyPos.Position;
            }
         }
         else if (selectedSetName == showedSetName)//dont have to update all keys, just the one specified
         {
            var keyPos = KeyPositions.Find(k => k.Key == key.ToString() && k.SetName == selectedSetName && k.IsActive); //only from selected setname will be in dictionary
            if (keyPos != null)
            {
               keysPositionDict[(Keys)key] = keyPos.Position;
            }
         }
      }

      public static void UpdateKeysSetName(string newSetName, string oldSetName)
      {
         KeyPositions.Where(k => k.SetName == oldSetName).ToList().ForEach(k =>
         {
            k.SetName = newSetName;
         });
      }

      public static void AddKeyToSelectedSetname(string key, Point position)
      {
         Keys Key = (Keys)Enum.Parse(typeof(Keys), key);
         if (!KeyPositions.Any(k => k.Key == key && k.SetName == selectedSetName)) // pokud klíč ještě neexistuje v daném setu
         {
            new KeyPos(key, position, selectedSetName, DateTime.Now, true);
            DBAccess.SaveOrUpdateKeyPos(Key, position, selectedSetName);
            UpdateKeyPosDict(Key);
            MessageBox.Show(
                $"Key '{key}' added to the set '{selectedSetName}' with coordinates {position}.",
                "Key Added",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
         }
         else // pokud klíč již existuje v daném setu - kontrola na duplikát nebo přepsání
         {
            var existing = KeyPositions.Find(k => k.Key == key && k.SetName == selectedSetName);
            if (existing.Position == position) // Klíč již existuje s těmito souřadnicemi
            {
               MessageBox.Show(
                   $"Key '{key}' already exists in the set '{selectedSetName}' with the same coordinates {position}.",
                   "Duplicate Key",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information
               );
               return; 
            }
            DialogResult result = MessageBox.Show(
                 $"Key '{key}' already exists in the set '{selectedSetName}' with coordinates {existing.Position}.\nDo you want to overwrite it with coordinates {position}?",
                 "Duplicate Key",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question
             );

            if (result == DialogResult.Yes) // Přepsat existující klíč
            {
               if (existing != null)
               {
                  existing.Position = position;
                  existing.CreatedAt = DateTime.Now;
                  existing.IsActive = true;
               }
               UpdateKeyPosDict(Key);
               DBAccess.SaveOrUpdateKeyPos(Key, position, selectedSetName);
            }
         }
      }

      /// <summary>
      /// Vymaže všechny klávesy z daného setu podle názvu setu.
      /// </summary>
      /// <param name="setname">název setu k vymazání kláves</param>
      public static void DeleteKeysBySetName(int setId, string setname)
      {
         KeyPositions.Where(k => k.SetName == setname).ToList().ForEach(k =>
         {
            KeyPositions.Remove(k);
         });
         setNames.Remove(setId); // odstraní setName z mapy setNames
      }

   }
}