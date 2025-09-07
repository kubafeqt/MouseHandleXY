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
      public static BaseKeys? selected; // the currently selected set of keys
      public static List<BaseKeys> BaseKeysList = new(); // for displaying data
      public static string actualSelectedSetName = "default"; // for manipulation with keys - stores the name of the set of keys
      public string SetName { get; set; }

      //then load it from db or use this default settings:
      private static Dictionary<Keys, MouseHandle.mouseActions> DefaultKeyToActionsDict = new Dictionary<Keys, MouseHandle.mouseActions>()
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

      public Dictionary<Keys, MouseHandle.mouseActions> KeyToActionsDict { get; set; }
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

      public static void AddNewBaseKeysDictionaries(BaseKeys bk)
      {
         if (bk.SetName == "default")
         {
            bk.KeyToActionsDict = DefaultKeyToActionsDict;
            bk.KeyActionsEnabledDict = DefaultKeyActionsEnabledDict; //then load this from db
         }
      }

      public static void LoadDefaultKeyActionsEnabledDict() //multiple tables for BaseKeys and EnabledActions (?) - probably not necessary
      {
         foreach (var action in DefaultKeyActionsEnabledDict.Keys.ToList())
         {
            DefaultKeyActionsEnabledDict[action] = DBAccess.LoadKeysActionsEnabledDict("default", action.ToString());
         }
      }


   }
}
