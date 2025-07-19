using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseXY
{
    class StartupManager
    {
      //// Import necessary Windows API functions
      //[DllImport("user32.dll")]
      //private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

      //[DllImport("user32.dll")]
      //private static extern bool SetForegroundWindow(IntPtr hWnd);

      //private const int SW_RESTORE = 9;

      //public static void BringOtherInstanceToFront()
      //{
      //   Process current = Process.GetCurrentProcess();
      //   foreach (Process process in Process.GetProcessesByName(current.ProcessName))
      //   {
      //      if (process.Id != current.Id)
      //      {
      //         IntPtr hWnd = process.MainWindowHandle;
      //         if (hWnd != IntPtr.Zero)
      //         {
      //            ShowWindowAsync(hWnd, SW_RESTORE);
      //            SetForegroundWindow(hWnd);
      //         }
      //         break;
      //      }
      //   }
      //}



      public static void SetStartup(bool enable, string appName, string appPath)
        {
            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                if (enable)
                {
                    rk.SetValue(appName, $"\"{appPath}\"");
                }
                else
                {
                    rk.DeleteValue(appName, false); // `false` zabrání výjimce, pokud hodnota neexistuje
                }
            }
        }

        public static bool IsInStartup(string appName)
        {
            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", false))
            {
                return rk.GetValue(appName) != null;
            }
        }
    }
}
