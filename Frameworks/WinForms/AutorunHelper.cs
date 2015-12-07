using Microsoft.Win32;
using System.Windows.Forms;

namespace OFClassLibrary.Frameworks.WinForms
{
    public class AutorunHelper
    {
        public static bool IsStartupItem(string appName)
        {
            using (var rkApp = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                return rkApp.GetValue(appName) != null;
            }
        }

        public static void AddToAutorun(string appName)
        {
            using(var rkApp = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                if (!IsStartupItem(appName))
                    rkApp.SetValue(appName, Application.ExecutablePath.ToString());
            }
        }

        public static void DeleteFromAutorun(string appName)
        {
            using (var rkApp = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                if (IsStartupItem(appName))
                    rkApp.DeleteValue(appName, false);
            }
        }
    }
}
