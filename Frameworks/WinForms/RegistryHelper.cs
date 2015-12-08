using Microsoft.Win32;
using System;

namespace OFClassLibrary.Frameworks.WinForms
{

    public static class RegistryHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool Has(RegistryHive target, string path, string name) => Read(target, path, name) != null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hive"></param>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object Read(RegistryHive hive, string path, string name)
        {
            using (RegistryKey registry = GetRegistry(hive))
            using (var rk = registry.OpenSubKey(@path, false))
            {
                return rk.GetValue(name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hive"></param>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void Write(RegistryHive hive, string path, string name, string value)
        {
            using (RegistryKey registry = GetRegistry(hive))
            using (var rk = registry.OpenSubKey(@path, true))
            {
                rk.SetValue(name, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hive"></param>
        /// <param name="path"></param>
        /// <param name="name"></param>
        public static void Delete(RegistryHive hive, string path, string name)
        {
            using (RegistryKey registry = GetRegistry(hive))
            using (var rk = registry.OpenSubKey(@path, true))
            {
                rk.DeleteValue(name, false);
            }
        }


        private static RegistryKey GetRegistry(RegistryHive hive) => RegistryKey.OpenBaseKey(hive, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);

    }
}
