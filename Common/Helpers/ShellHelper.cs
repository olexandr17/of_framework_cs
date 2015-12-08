using System;
using System.Diagnostics;
using System.Threading;

namespace OFClassLibrary.Common.Helpers
{
    public static class ShellHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static string RunCommand(string fileName, string arguments)
        {
            string result = string.Empty;

            Thread thread = new Thread(data =>
            {
                ProcessStartInfo psInfo = new ProcessStartInfo(@fileName, @data.ToString());
                psInfo.WindowStyle = ProcessWindowStyle.Hidden;
                psInfo.RedirectStandardOutput = true;
                psInfo.UseShellExecute = false;
                psInfo.CreateNoWindow = true;
                psInfo.ErrorDialog = true;

                Process process = Process.Start(psInfo);
                result = process.StandardOutput.ReadToEnd();

                process.WaitForExit();
            });
            thread.Start(arguments);
            thread.Join();

            return result;
        }

    }
}
