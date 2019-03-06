using System;
using System.Linq;

public static class Extensions
{
    public static int? GetEveOnlineClientProcessId() =>
        System.Diagnostics.Process.GetProcesses()
            ?.FirstOrDefault(process =>
            {
                try
                {
                    return string.Equals("ExeFile.exe", process?.MainModule?.ModuleName,
                        StringComparison.InvariantCultureIgnoreCase);
                }
                catch
                {
                }

                return false;
            })
            ?.Id;

    public static IntPtr? GetEveMainWindow() =>
        System.Diagnostics.Process.GetProcesses()
            ?.FirstOrDefault(process =>
            {
                try
                {
                    return string.Equals("ExeFile.exe", process?.MainModule?.ModuleName,
                        StringComparison.InvariantCultureIgnoreCase);
                }
                catch
                {
                }

                return false;
            })
            ?.MainWindowHandle;
    public static IntPtr? GetEveMainWindow(string Player) =>
        System.Diagnostics.Process.GetProcesses()
            ?.FirstOrDefault(process =>
            {
                try
                {
                    return string.Equals("ExeFile.exe", process?.MainModule?.ModuleName,
                        StringComparison.InvariantCultureIgnoreCase);
                }
                catch
                {
                }

                return false;
            })
            ?.MainWindowHandle;


    public static string Julian4() =>string.Format(" {0}{1}",DateTime.Now.ToString("yy").Substring(1), DateTime.Now.DayOfYear.ToString("000"));
  

};