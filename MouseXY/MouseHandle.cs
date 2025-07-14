using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace MouseXY
{
    class MouseHandle
    {

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        [DllImport("user32.dll")]
        public static extern bool ShowCursor(bool bShow);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        static void Main()
        {
            Application.EnableVisualStyles();
            _hookID = SetHook(_proc);
            //ApplicationConfiguration.Initialize();
            Application.Run(new Form1()); // Nevyžaduje WinForm, ale drží aplikaci naživu
            UnhookWindowsHookEx(_hookID);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
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


        private const int WM_KEYUP = 0x0101;

        private static void LeftMouseClick(IntPtr wParam)
        {
            if (wParam == (IntPtr)WM_KEYDOWN)
            {
                // Levé tlačítko myši dolů
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
            }
            else if (wParam == (IntPtr)WM_KEYUP)
            {
                // Levé tlačítko myši nahoru
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
            }
        }

        private static void RightMouseClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, UIntPtr.Zero);
        }

        static int step = 10;
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Point pos = Cursor.Position;


                switch ((Keys)vkCode)
                {
                    case Keys.LControlKey:
                        {
                            ControlMethod(wParam);
                            break;
                        }

                    case Keys.LShiftKey:
                        {
                            ShiftMethod(wParam);
                            break;
                        }
                }

                if (mouseCursor)
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
                                LeftMouseClick(wParam);
                                return (IntPtr)1;
                            }
                        case Keys.Q:
                            {
                                RightMouseClick();
                                return (IntPtr)1;
                            }
                    }
                }
            }

            // ostatní klávesy propustit dál
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        static bool mouseCursor = false;
        static Stopwatch stopwatch = Stopwatch.StartNew();
        static bool firstPress = false;
        private static void ControlMethod(IntPtr wParam)
        {
            if (wParam != (IntPtr)WM_KEYDOWN) return;

            if (stopwatch.ElapsedMilliseconds < 250)
            {
                mouseCursor = !mouseCursor;
                Sounds.PlaySound(mouseCursor);
            }
            stopwatch.Restart();
        }

        static DateTime dateTime = DateTime.Now;
        private static void ShiftMethod(IntPtr wParam)
        {
            if (wParam != (IntPtr)WM_KEYDOWN) return;

            if (DateTime.Now.Subtract(dateTime).TotalMilliseconds < 250)
            {
                step = step == 10 ? 2 : 10;
            }
            dateTime = DateTime.Now;
        }

    }
}
