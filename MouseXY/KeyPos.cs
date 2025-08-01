using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseXY
{
   class KeyPos
   {
      
      public static List<KeyPos> KeyPositions = new(); //pro zobrazování dat
      public static Dictionary<Keys, Point> keysPositionDict { get; private set; } = new();
      //pro manipulaci s klávesy - stores the position of the mouse for each key
      public static Dictionary<int, string> setNames = new();
      public static string selectedSetName = "default"; // pro manipulaci s klávesy - stores the name of the set of keys
      public static string showedSetName = "default"; // pro zobrazení v UI, aby se neukazoval default setName

      //Setname: TODO ->
      // uložit/načíst z db
      // - load selected setname  
      // - save selected setname
      // - delete selected setname and make it default
      // - 
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
         if (!KeyPositions.Any(k => k.Key == key && k.SetName == setName)) //key in setame does not exist - add new
         {
            KeyPositions.Add(this);
            if (setName == selectedSetName)
            {
               keysPositionDict.Add((Keys)Enum.Parse(typeof(Keys), key), position);
            }
         }
         else //key in setName exists - edit
         {
            var existingKeyPos = KeyPositions.Find(k => k.Key == key && k.SetName == setName);
            if (existingKeyPos != null)
            {
               existingKeyPos.Position = position;
               existingKeyPos.SetName = setName;
               existingKeyPos.CreatedAt = createdAt;
               existingKeyPos.IsActive = isActive;
               if (setName == selectedSetName)
               {
                  keysPositionDict[(Keys)Enum.Parse(typeof(Keys), key)] = position; // update the position in the dictionary
               }
            }
         }
      }

      public static void UpdateKeyPosition(string key, Point position, bool selectedSetname = true) // updates the position of the key in the selected set
      {
         string setName = selectedSetname ? selectedSetName : showedSetName;
         var existingKeyPos = KeyPositions.Find(k => k.Key == key && k.SetName == setName);
         if (existingKeyPos != null)
         {
            existingKeyPos.Position = position;
            if (selectedSetname)
            {
               keysPositionDict[(Keys)Enum.Parse(typeof(Keys), key)] = position; // update the position in the dictionary
            }
         }
      }

      public static void UpdateKeyPosDict()
      {
         keysPositionDict.Clear();
         foreach (var keyPos in KeyPositions.Where(k => k.SetName == selectedSetName && k.IsActive))
         {
            keysPositionDict[(Keys)Enum.Parse(typeof(Keys), keyPos.Key)] = keyPos.Position;
         }
      }

      public static void AddKeyToSelectedSetname(string key, Point position)
      {
         Keys Key = (Keys)Enum.Parse(typeof(Keys), key);
         if (!KeyPositions.Any(k => k.Key == key && k.SetName == selectedSetName))
         {
            new KeyPos(key, position, selectedSetName, DateTime.Now, true);
            DBAccess.SaveOrUpdateKeyPos(Key, position, selectedSetName); // save to database
            UpdateKeyPosDict();
            //keysPositionDict[(Keys)Enum.Parse(typeof(Keys), key)] = position; // add to the dictionary
            MessageBox.Show(
                $"Key '{key}' added to the set '{selectedSetName}' with coordinates {position}.",
                "Key Added",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
         }
         else
         {
            var existing = KeyPositions.Find(k => k.Key == key && k.SetName == selectedSetName);
            if (existing.Position == position)
            {
               MessageBox.Show(
                   $"Key '{key}' already exists in the set '{selectedSetName}' with the same coordinates {position}.",
                   "Duplicate Key",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information
               );
               return; // Klíč již existuje s těmito souřadnicemi, nic nedělej
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
               //keysPositionDict[Key] = position;
               UpdateKeyPosDict();
               DBAccess.SaveOrUpdateKeyPos(Key, position, selectedSetName);
            }
         }
      }

      //public static void ClearKeyPositions()
      //{
      //   KeyPositions.Clear();
      //   keysPositionDict.Clear();
      //}

      //public static void GetKeyPositions(string setName = "load")
      //{
      //   if (setName == "load")
      //   {
      //      keysPositionDict.Clear();
      //      foreach (var keyPos in KeyPositions)
      //      {
      //         if (keyPos.IsActive)
      //         {
      //            keysPositionDict[(Keys)Enum.Parse(typeof(Keys), keyPos.Key)] = keyPos.Position;
      //         }
      //      }
      //   }
      //   else
      //   {
      //      keysPositionDict.Clear();
      //      foreach (var keyPos in KeyPositions.Where(k => k.SetName == setName && k.IsActive))
      //      {
      //         keysPositionDict[(Keys)Enum.Parse(typeof(Keys), keyPos.Key)] = keyPos.Position;
      //      }
      //   }
      //}

   }
}
