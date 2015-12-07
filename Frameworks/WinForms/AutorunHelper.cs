using Microsoft.Win32;
using System.Windows.Forms;

namespace OFClassLibrary.Frameworks.WinForms
{
    public static class AutorunHelper
    {

        public static string KeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public static bool IsAutorun(string appName)
        {
            using (var rkApp = Registry.CurrentUser.OpenSubKey(KeyPath, true))
            {
                return rkApp.GetValue(appName) != null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appName"></param>
        public static void AddAutorun(string appName)
        {
            using(var rkApp = Registry.CurrentUser.OpenSubKey(KeyPath, true))
            {
                if (!IsAutorun(appName))
                    rkApp.SetValue(appName, Application.ExecutablePath.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appName"></param>
        public static void DeleteAutorun(string appName)
        {
            using (var rkApp = Registry.CurrentUser.OpenSubKey(KeyPath, true))
            {
                if (IsAutorun(appName))
                    rkApp.DeleteValue(appName, false);
            }
        }
    }
}
