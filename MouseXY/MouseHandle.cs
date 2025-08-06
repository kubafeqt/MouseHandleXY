using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MouseXY
{
   class MouseHandle
   {
      #region Imports from user32.dll and kernel32.dll
      private const int WH_KEYBOARD_LL = 13;
      private const int WM_KEYDOWN = 0x0100;
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

      private const int WM_KEYUP = 0x0101;

      #endregion

      #region Mouse Control Methods
      private static bool isLeftMouseDown = false;
      private static void LeftMouseDown(IntPtr wParam)
      {
         if (wParam == (IntPtr)WM_KEYDOWN && !isLeftMouseDown) // Levé tlačítko myši dolů
         {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
            isLeftMouseDown = true;
         }
         else if (wParam == (IntPtr)WM_KEYUP && isLeftMouseDown) // Levé tlačítko myši nahoru
         {
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
            isLeftMouseDown = false;
         }
      }

      private static void RightMouseClick()
      {
         mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, UIntPtr.Zero);
         mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, UIntPtr.Zero);
      }

      private static bool isMiddleMouseDown = false;
      private static void MiddleMouseDown(IntPtr wParam)
      {
         if (wParam == (IntPtr)WM_KEYDOWN && !isMiddleMouseDown) // Stisk prostředního tlačítka myši
         {
            mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, UIntPtr.Zero);
            isMiddleMouseDown = true;
         }
         else if (wParam == (IntPtr)WM_KEYUP && isMiddleMouseDown) // Uvolnění prostředního tlačítka myši
         {
            mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, UIntPtr.Zero);
            isMiddleMouseDown = false;
         }
      }

      //public static bool middleMouseHeld = false;
      //public static void MiddleMouseHeld(IntPtr wParam)
      //{
      //   if (wParam == (IntPtr)WM_KEYDOWN)
      //   {
      //      middleMouseHeld = !middleMouseHeld;

      //      if (middleMouseHeld)
      //      {
      //         mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, UIntPtr.Zero);
      //      }
      //      else
      //      {
      //         mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, UIntPtr.Zero);
      //      }
      //   }
      //}

      #endregion

      #region Mouse Cursor Control by keyboard and Key Positioning set
      public static bool setKeyToPos = false; // nastaví, zda se má ukládat pozice klávesy - sets whether to save the key position
      static List<Keys> registeredKeys = new() // list of registered keys which cannot be set to position of mouse cursor
      {
         Keys.Up, Keys.Down, Keys.Left, Keys.Right,
         Keys.W, Keys.A, Keys.S, Keys.D,
         Keys.E, Keys.Q, Keys.R, Keys.F,
         Keys.LControlKey, Keys.LShiftKey,
         //control keys:
         Keys.LMenu, Keys.LWin, Keys.RControlKey,
         Keys.RShiftKey, Keys.RMenu, Keys.RWin,
         Keys.Space, Keys.Tab, Keys.Enter,
         Keys.Escape, Keys.Back, Keys.Delete,
         Keys.CapsLock, Keys.Scroll, Keys.Pause,
         Keys.Insert, Keys.Home, Keys.End,
         Keys.PageUp, Keys.PageDown, Keys.PrintScreen,
         Keys.NumLock
      };

      static int step = 10;
      private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam) //captures key presses
      {
         if (nCode >= 0)
         {
            int vkCode = Marshal.ReadInt32(lParam);
            Point pos = Cursor.Position;

            switch ((Keys)vkCode)
            {
               //open/close mouse control by keyboard
               case Keys.LControlKey:
                  {
                     ControlMethod(wParam);
                     break;
                  }
               //double shift for change speed of mouse step
               case Keys.LShiftKey:
                  {
                     ShiftMethod(wParam);
                     break;
                  }
            }

            if (mouseCursorHandle) //when mouse control by keyboard is enabled
            {
               switch ((Keys)vkCode)
               {
                  case Keys.Up or Keys.W:
                     SetCursorPos(pos.X, pos.Y - step);
                     return (IntPtr)1; //Blokuje klávesu
                  case Keys.Down or Keys.S:
                     SetCursorPos(pos.X, pos.Y + step);
                     return (IntPtr)1;
                  case Keys.Left or Keys.A:
                     SetCursorPos(pos.X - step, pos.Y);
                     return (IntPtr)1;
                  case Keys.Right or Keys.D:
                     SetCursorPos(pos.X + step, pos.Y);
                     return (IntPtr)1;
                  case Keys.E:
                     {
                        LeftMouseDown(wParam); //držení levého tlačítka myši
                        //if (middleMouseHeld) //pokud je prostřední tlačítko myši drženo
                        //{
                        //   MiddleMouseHeld(wParam); //přepne držení prostředního tlačítka myši - důležitý, jinak se hodně může zaseknout
                        //}
                        return (IntPtr)1;
                     }
                  case Keys.Q:
                     {
                        RightMouseClick(); //kliknutí pravým tlačítkem myši
                        //if (middleMouseHeld) //pokud je prostřední tlačítko myši drženo
                        //{
                        //   MiddleMouseHeld(wParam); //přepne držení prostředního tlačítka myši - důležitý, jinak se hodně může zaseknout
                        //}
                        return (IntPtr)1;
                     }
                  case Keys.R or Keys.F:
                     {
                        MiddleMouseDown(wParam); //držení prostředního tlačítka myši
                        return (IntPtr)1;
                     }
                  //case Keys.F:
                  //   {
                  //      MiddleMouseHeld(wParam); //přepíná držení prostředního tlačítka myši
                  //      return (IntPtr)1;
                  //   }
               }

               if (KeyPos.keysPositionDict.Count > 0 && KeyPos.keysPositionDict.ContainsKey((Keys)vkCode)) // pokud je klávesa již v mapě, přesunout myš na její pozici
               {
                  KeyPos k = KeyPos.KeyPositions.Find(k => k.Key == ((Keys)vkCode).ToString());
                  if (k != null && k.IsActive) // pokud je klávesa aktivní
                  {
                     Point keyPos = KeyPos.keysPositionDict[(Keys)vkCode];
                     SetCursorPos(keyPos.X, keyPos.Y);
                     return (IntPtr)1; // Blokuje klávesu
                  }
               }
            }
            else //save position of key to mouse cursor
            {
               Keys key = (Keys)vkCode;
               if (setKeyToPos && ((vkCode >= 0x30 && vkCode <= 0x39) || !registeredKeys.Contains(key))) // čísla 0-9 nebo jiné klávesy, které nejsou registrovány
               {
                  KeyPos.CreateUpdateKeyPosition(key.ToString(), pos); // aktualizovat pozici v objektu KeyPos a seznamu KeyPosList
                  DBAccess.SaveOrUpdateKeyPos(key, pos, KeyPos.showedSetName); // uložit pozici do databáze
                  OnSetKeyToPos?.Invoke(); // invoke event to set key to show keys positions in datagridview and SetKeyPos()
                  return (IntPtr)1; // Blokuje klávesu
               }
            }
         }

         // ostatní klávesy propustit dál
         return CallNextHookEx(_hookID, nCode, wParam, lParam);
      }

      public static Action OnSetKeyToPos; // event for set key to position of mouse cursor


      #endregion

      #region Open/Close Mouse Control by Keyboard
      public static event Action<bool> OnMouseCursorHandleOpenChanged; // event when change mouseCursor property for enable/disable button to set key position
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
      static Stopwatch stopwatch = Stopwatch.StartNew();
      private static void ControlMethod(IntPtr wParam) //open/close mouse control by keyboard
      {
         if (wParam != (IntPtr)WM_KEYDOWN) return;

         if (stopwatch.ElapsedMilliseconds < Settings.delayMs)
         {
            mouseCursorHandle = !mouseCursorHandle;
            Sounds.PlaySound(mouseCursorHandle);
         }
         stopwatch.Restart();
      }

      static DateTime dateTime = DateTime.Now;
      private static void ShiftMethod(IntPtr wParam) //change mouse step speed
      {
         if (wParam != (IntPtr)WM_KEYDOWN) return;

         if (DateTime.Now.Subtract(dateTime).TotalMilliseconds < Settings.delayMs)
         {
            step = step == 10 ? 2 : 10;
         }
         dateTime = DateTime.Now;
      }

      #endregion

   }
}