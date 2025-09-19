using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MouseXY
{
   class MouseHandle
   {
      #region Imports from user32.dll and kernel32.dll
      private const int WH_KEYBOARD_LL = 13;
      private const int WM_KEYDOWN = 0x0100;
      private const int WM_KEYUP = 0x0101;
      private const int WM_SYSKEYDOWN = 0x0104;
      private const int WM_SYSKEYUP = 0x0105;
      private const int MOUSEEVENTF_WHEEL = 0x0800;

      public static LowLevelKeyboardProc _proc = HookCallback;
      public static IntPtr _hookID = IntPtr.Zero;

      [DllImport("user32.dll")]
      public static extern bool ShowCursor(bool bShow);

      [DllImport("user32.dll")]
      private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

      [DllImport("user32.dll")]
      public static extern bool UnhookWindowsHookEx(IntPtr hhk);

      [DllImport("user32.dll")]
      private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

      [DllImport("kernel32.dll")]
      private static extern IntPtr GetModuleHandle(string lpModuleName);

      [DllImport("user32.dll")]
      private static extern bool SetCursorPos(int X, int Y);

      public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

      public static IntPtr SetHook(LowLevelKeyboardProc proc)
      {
         using (Process curProcess = Process.GetCurrentProcess())
         using (ProcessModule curModule = curProcess.MainModule)
         {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule.ModuleName), 0);
         }
      }

      [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
      public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, UIntPtr dwExtraInfo);

      private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
      private const uint MOUSEEVENTF_LEFTUP = 0x0004;
      private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
      private const uint MOUSEEVENTF_RIGHTUP = 0x0010;
      private const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
      private const uint MOUSEEVENTF_MIDDLEUP = 0x0040;

      #endregion

      #region Mouse Control Methods
      private static bool isLeftMouseDown = false; //pro neopakování stisku levého tlačítka myši
      private static void LeftMouseDown(IntPtr wParam)
      {
         if (wParam == WM_KEYDOWN && !isLeftMouseDown) // Levé tlačítko myši dolů
         {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
            isLeftMouseDown = true;
         }
         else if (wParam == WM_KEYUP && isLeftMouseDown) // Levé tlačítko myši nahoru
         {
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
            isLeftMouseDown = false;
         }
      }

      private static bool isRightMouseDown = false;
      private static void RightMouseDown(IntPtr wParam)
      {
         if (wParam == WM_KEYDOWN && !isRightMouseDown)
         {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, UIntPtr.Zero);
            isRightMouseDown = true;
         }
         else if (wParam == WM_KEYUP && isRightMouseDown)
         {
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, UIntPtr.Zero);
            isRightMouseDown = false;
         }
      }

      private static bool isMiddleMouseDown = false;
      private static void MiddleMouseDown(IntPtr wParam)
      {
         if (wParam == WM_KEYDOWN && !isMiddleMouseDown) // Stisk prostředního tlačítka myši
         {
            mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, UIntPtr.Zero);
            isMiddleMouseDown = true;
         }
         else if (wParam == WM_KEYUP && isMiddleMouseDown) // Uvolnění prostředního tlačítka myši
         {
            mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, UIntPtr.Zero);
            isMiddleMouseDown = false;
         }
      }

      private static void MiddleMouseWheelDown(IntPtr wParam)
      {
         //MidleMouseWheelDown:
         if (wParam == WM_KEYDOWN)
         {
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, unchecked((uint)-120), UIntPtr.Zero); // -120: one notch down
         }

      }

      private static void MiddleMouseWheelUp(IntPtr wParam)
      {
         //MidleMouseWheelUp:
         if (wParam == WM_KEYDOWN)
         {
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, 120, UIntPtr.Zero); // 120: one notch up
         }
      }
      #endregion

      #region Mouse Cursor Control by keyboard and Key Positioning set
      public static bool setKeyToPos = false; // nastaví, zda se má ukládat pozice klávesy - sets whether to save the key position
      static List<Keys> registeredKeys = new() // list of registered keys which cannot be set to position of mouse cursor
      {
         //Keys.Up, Keys.Down, Keys.Left, Keys.Right,
         //Keys.W, Keys.A, Keys.S, Keys.D,
         //Keys.E, Keys.Q, Keys.R, Keys.F, Keys.C,
         Keys.LControlKey, Keys.LShiftKey, Keys.LMenu,
         //control keys:
         Keys.LWin, Keys.RControlKey,
         Keys.RShiftKey, Keys.RMenu, Keys.RWin,
         Keys.Space, Keys.Tab, Keys.Enter,
         Keys.Escape, Keys.Back, Keys.Delete,
         Keys.CapsLock, Keys.Scroll, Keys.Pause,
         Keys.Insert, Keys.Home, Keys.End,
         Keys.PageUp, Keys.PageDown, Keys.PrintScreen,
         Keys.NumLock
      };

      public enum mouseActions
      {
         goUp,
         goDown,
         goLeft,
         goRight,
         leftMouseClick,
         rightMouseClick,
         middleMouseClick,
         middleMouseWheelUp,
         middleMouseWheelDown
      }

      static int step = Settings.normalSpeed;
      private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam) //captures key presses
      {
         if (nCode >= 0)
         {
            int vkCode = Marshal.ReadInt32(lParam);
            Point pos = Cursor.Position;
            Keys key = (Keys)vkCode;

            if ((int)wParam == WM_KEYUP)  // akce jen při puštění
            {
               switch (key)
               {
                  //double ctrl for open/close mouse control by keyboard
                  case Keys.LControlKey:
                     {
                        ControlMethod(wParam);
                        break;
                     }
                  //double shift to change speed of mouse step to slow
                  case Keys.LShiftKey:
                     {
                        ShiftMethod(wParam);
                        break;
                     }
                  //double left alt to change speed of mouse step to fast
                  case Keys.LMenu:
                     {
                        JumpMethod(wParam);
                        break;
                     }
               }
            }

            if (mouseCursorHandle) //when mouse control by keyboard is enabled
            {
               var KTAD = BaseKeys.selected?.KeysToActionDict;
               if (BaseKeys.selected != null && KTAD.ContainsKey(key) && BaseKeys.selected.KeyActionsEnabledDict[KTAD[key]])
               {
                  switch (KTAD[key])
                  {
                     case mouseActions.goUp:
                        {
                           SetCursorPos(pos.X, pos.Y - step);
                           return (IntPtr)1; //Blokuje klávesu
                        }
                     case mouseActions.goDown:
                        {
                           SetCursorPos(pos.X, pos.Y + step);
                           return (IntPtr)1;
                        }
                     case mouseActions.goLeft:
                        {
                           SetCursorPos(pos.X - step, pos.Y);
                           return (IntPtr)1;
                        }
                     case mouseActions.goRight:
                        {
                           SetCursorPos(pos.X + step, pos.Y);
                           return (IntPtr)1;
                        }
                     case mouseActions.leftMouseClick:
                        {
                           LeftMouseDown(wParam); //držení levého tlačítka myši
                           return (IntPtr)1;
                        }
                     case mouseActions.rightMouseClick:
                        {
                           RightMouseDown(wParam); //kliknutí pravým tlačítkem myši
                           return (IntPtr)1;
                        }
                     case mouseActions.middleMouseWheelUp:
                        {
                           MiddleMouseWheelUp(wParam); //posun kolečkem myši nahoru
                           return (IntPtr)1;
                        }
                     case mouseActions.middleMouseWheelDown:
                        {
                           MiddleMouseWheelDown(wParam); //posun kolečkem myši dolů
                           return (IntPtr)1;
                        }
                     case mouseActions.middleMouseClick:
                        {
                           MiddleMouseDown(wParam); //kliknutí prostředním tlačítkem myši
                           return (IntPtr)1;
                        }
                  }
               }

               if (KeyPos.KeysPositionDict.Count > 0 && KeyPos.KeysPositionDict.ContainsKey(key)) // pokud je klávesa již v mapě, přesunout myš na její pozici
               {
                  KeyPos? k = KeyPos.KeyPositionsList.Find(k => k.Key == (key).ToString());
                  if (k != null && k.IsActive) // pokud je klávesa aktivní
                  {
                     Point keyPos = KeyPos.KeysPositionDict[key];
                     SetCursorPos(keyPos.X, keyPos.Y);
                     return (IntPtr)1; // Blokuje klávesu
                  }
               }
            }
            else //save position of key to mouse cursor
            {
               if (setKeyToPos && ((vkCode >= 0x30 && vkCode <= 0x39) || !registeredKeys.Contains(key))) // čísla 0-9 nebo jiné klávesy, které nejsou registrovány
               {
                  KeyPos.CreateUpdateKeyPosition(key.ToString(), pos); // aktualizovat pozici v objektu KeyPos a seznamu KeyPosList
                  DBAccess.SaveOrUpdateKeyPos(key, pos, KeyPos.showedSetName); // uložit pozici do databáze
                  OnSetKeyToPos?.Invoke(); // invoke event to set key to show keys positions in datagridview and SetKeyPos()
                  return (IntPtr)1;
               }
            }
         }

         // ostatní klávesy propustit dál
         return CallNextHookEx(_hookID, nCode, wParam, lParam);
      }

      public static Action? OnSetKeyToPos; // event for set key to position of mouse cursor

      #endregion

      #region Open/Close Mouse Control by Keyboard
      public static event Action<bool>? OnMouseCursorHandleOpenChanged; // event when change mouseCursor property for enable/disable button to set key position
      private static bool _mouseCursorHandle = false;
      public static bool mouseCursorHandle // property for enable/disable mouse control by keyboard
      {
         get => _mouseCursorHandle;
         set
         {
            if (_mouseCursorHandle != value)
            {
               _mouseCursorHandle = value;
               OnMouseCursorHandleOpenChanged?.Invoke(value);
            }
         }
      }

      static DateTime dateTime = DateTime.Now;
      private static void ControlMethod(IntPtr wParam) //open/close mouse control by keyboard
      {
         if (wParam != (IntPtr)WM_KEYUP) return;

         if (DateTime.Now.Subtract(dateTime).TotalMilliseconds < Settings.delayMs)
         {
            mouseCursorHandle = !mouseCursorHandle;
            Sounds.PlayDefSound(mouseCursorHandle);
         }
         dateTime = DateTime.Now;
      }

      private static void ShiftMethod(IntPtr wParam) //change mouse step speed to slower
      {
         if (wParam != (IntPtr)WM_KEYUP) return;

         if (mouseCursorHandle)// && stopwatch.ElapsedMilliseconds < Settings.delayMs)
         {
            step = step == Settings.normalSpeed ? Settings.slowSpeed : Settings.normalSpeed; //Slowing down fastSpeed and normalSpeed, fasting up slowSpeed
         }
      }

      static int latestSpeed = step;
      private static void JumpMethod(IntPtr wParam) //change mouse step speed to faster
      {
         if (wParam != (IntPtr)WM_KEYUP) return;

         if (mouseCursorHandle)// && DateTime.Now.Subtract(dtJumpMethod).TotalMilliseconds < Settings.delayMs)
         {
            bool fastSpeed = step == Settings.fastSpeed;
            latestSpeed = fastSpeed ? latestSpeed : step;
            step = fastSpeed ? latestSpeed : Settings.fastSpeed; //modify slowSpeed and normalSpeed to fastSpeed, slowing down fastSpeed    
         }
      }

      #endregion

   }
}