using Microsoft.Win32;
using System.Windows.Forms;

namespace OFClassLibrary.Frameworks.WinForms
{
    public static class AutorunHelper
    {

        private static readonly RegistryHive Hive = RegistryHive.CurrentUser;
        private const string KeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public static bool IsAutorun(string appName) => RegistryHelper.Has(Hive, KeyPath, appName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appName"></param>
        public static void AddAutorun(string appName) => RegistryHelper.Write(Hive, KeyPath, appName, Application.ExecutablePath.ToString());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appName"></param>
        public static void DeleteAutorun(string appName) => RegistryHelper.Delete(Hive, KeyPath, appName);
    }
}
