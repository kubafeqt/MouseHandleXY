using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseXY
{
   /// <summary>
   /// Přepíše klasický setNames
   /// 
   /// </summary>
   internal class BaseKeys
   {
      public static BaseKeys? selected; // the currently selected set of base keys
      public static BaseKeys? showed; // the currently showed set of base keys
      public static List<BaseKeys> BaseKeysList = new(); // for displaying data
      public static string actualSelectedSetName = "default"; // for manipulation with keys - stores the name of the set of keys
      public static string actualShowedSetName = "default";
      public string SetName { get; set; }

      public bool Changed { get; set; } = false;

      //then load it from db or use this default settings:
      private static Dictionary<Keys, MouseHandle.mouseActions> DefaultKeysToActionDict = new Dictionary<Keys, MouseHandle.mouseActions>()
      {
         { Keys.W, MouseHandle.mouseActions.goUp },
         { Keys.S, MouseHandle.mouseActions.goDown },
         { Keys.A, MouseHandle.mouseActions.goLeft },
         { Keys.D, MouseHandle.mouseActions.goRight },
         { Keys.Up, MouseHandle.mouseActions.goUp },
         { Keys.Down, MouseHandle.mouseActions.goDown },
         { Keys.Left, MouseHandle.mouseActions.goLeft },
         { Keys.Right, MouseHandle.mouseActions.goRight },
         { Keys.E, MouseHandle.mouseActions.leftMouseClick },
         { Keys.Q, MouseHandle.mouseActions.rightMouseClick },
         { Keys.R, MouseHandle.mouseActions.middleMouseWheelUp },
         { Keys.F, MouseHandle.mouseActions.middleMouseWheelDown },
         { Keys.C, MouseHandle.mouseActions.middleMouseClick }
      };

      private static Dictionary<MouseHandle.mouseActions, bool> DefaultKeyActionsEnabledDict = new Dictionary<MouseHandle.mouseActions, bool>()
      {
         { MouseHandle.mouseActions.goUp, true },
         { MouseHandle.mouseActions.goDown, true },
         { MouseHandle.mouseActions.goLeft, true },
         { MouseHandle.mouseActions.goRight, true },
         { MouseHandle.mouseActions.leftMouseClick, true },
         { MouseHandle.mouseActions.rightMouseClick, true },
         { MouseHandle.mouseActions.middleMouseClick, true },
         { MouseHandle.mouseActions.middleMouseWheelUp, true },
         { MouseHandle.mouseActions.middleMouseWheelDown, true }
      };

      public Dictionary<Keys, MouseHandle.mouseActions> KeysToActionDict { get; set; }
      public Dictionary<MouseHandle.mouseActions, List<Keys>> ActionsToKeysDict { get; set; }
      public Dictionary<MouseHandle.mouseActions, bool> KeyActionsEnabledDict { get; set; }

      public BaseKeys(string setname)
      {
         SetName = setname;
         BaseKeysList.Add(this);
         AddNewBaseKeysDictionaries(this);
      }

      public static void ChangeSelectedBaseKeys(string setName)
      {
         actualSelectedSetName = setName;
         selected = BaseKeysList.Find(bk => bk.SetName == setName);
      }

      public static void ChangeShowedBaseKeys(string setName)
      {
         actualShowedSetName = setName;
         showed = BaseKeysList.Find(bk => bk.SetName == setName);
      }

      public static void AddNewBaseKeysDictionaries(BaseKeys bk)
      {
         if (bk.SetName == "default")
         {
            bk.KeysToActionDict = DefaultKeysToActionDict;
            bk.KeyActionsEnabledDict = DefaultKeyActionsEnabledDict;
         }
         else //basic, then load from db
         {             
            bk.KeysToActionDict = new Dictionary<Keys, MouseHandle.mouseActions>();
            bk.KeyActionsEnabledDict = new Dictionary<MouseHandle.mouseActions, bool>();
            bk.ActionsToKeysDict = new Dictionary<MouseHandle.mouseActions, List<Keys>>();
         }
      }

      public static void LoadDefaultKeyActionsEnabledDict() //multiple tables for BaseKeys and EnabledActions (?) - probably not necessary
      {
         foreach (var action in DefaultKeyActionsEnabledDict.Keys.ToList())
         {
            DefaultKeyActionsEnabledDict[action] = DBAccess.LoadKeysActionsEnabledDict("default", action.ToString());
         }
      }

      public static void LoadKeyActionsEnabled()
      {
         foreach (var basekeys in BaseKeysList.Where(p => !p.SetName.Equals("default", StringComparison.OrdinalIgnoreCase)).ToList())
         {
            foreach (MouseHandle.mouseActions action in Enum.GetValues(typeof(MouseHandle.mouseActions)))
            {
               basekeys.KeyActionsEnabledDict[action] = DBAccess.LoadKeysActionsEnabledDict(basekeys.SetName, action.ToString());
            }
         }
      }

      //public static void LoadActionsToKeysDict()
      //{
      //   foreach (var basekeys in BaseKeysList)
      //   {
      //      basekeys.ActionsToKeysDict = new Dictionary<MouseHandle.mouseActions, List<Keys>>();
      //      foreach (var action in Enum.GetValues(typeof(MouseHandle.mouseActions)).Cast<MouseHandle.mouseActions>())
      //      {
      //         basekeys.ActionsToKeysDict[action] = basekeys.KeysToActionDict.Where(kvp => kvp.Value == action).Select(kvp => kvp.Key).ToList();
      //      }
      //   }
      //}

      public bool CheckAssignedKeyEnabled(Keys key)
      {
         if (KeysToActionDict.TryGetValue(key, out MouseHandle.mouseActions action))
         {
            return KeyActionsEnabledDict.TryGetValue(action, out bool isEnabled) && isEnabled;
         }
         return false;
      }

      public void SaveBaseKeysToDB()
      {
         Changed = false;
         foreach (var kvp in ActionsToKeysDict)
         {
           DBAccess.SaveBaseKeysToDB(kvp.Key.ToString(), kvp.Value[0].ToString(), kvp.Value[1].ToString(), KeyActionsEnabledDict[kvp.Key], SetName);
         }
      }

   }
}
