using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseXY
{
    class StartupManager
    {
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
