using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Magic
{
    internal static class MTGArena
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        static IntPtr hWnd;
        public static void StartMTGArena()
        {
            ConfigReader config = ConfigReader.Current;
            string steamPath = config.GetExpandedSteamPath();
            Process mgta = new Process();

            mgta.StartInfo.FileName = steamPath;
            mgta.StartInfo.Arguments = "-applaunch " + config.SteamAppId;

            if (config.DryRun)
            {
                LogFile.WriteLog($"DRY RUN: Would start MTG Arena with {steamPath} {mgta.StartInfo.Arguments}");
                return;
            }

            mgta.Start();

            while (!LookAndClick.LookForLoginScreen())
            {
                Thread.Sleep(500);
                GetMagicWindowInFocus();
                Thread.Sleep(500);
            }
        }

        public static bool IsMTGArenaRunning()
        {
            var pross = Process.GetProcesses();
            foreach (var proc in pross)
            {
                if (proc.ProcessName.IndexOf("MTGA") != -1)
                {
                    return true;
                }
            }
            return false;
        }



        public static void GetMagicWindowInFocus()
        {
            var pross = Process.GetProcesses();
            foreach (var proc in pross)
            {
                if (proc.ProcessName.IndexOf("MTGA") != -1)
                {
                    hWnd = proc.MainWindowHandle;
                    if (hWnd != IntPtr.Zero)
                    {
                        SetForegroundWindow(hWnd);
                    }
                }
            }
        }

        public static bool CloseMTGArena()
        {
            var pross = Process.GetProcesses();
            foreach (var proc in pross)
            {
                if (proc.ProcessName == "MTGA")
                {
                    if (!ConfigReader.Current.AllowForceKillArena)
                    {
                        LogFile.WriteLog("CloseMTGArena skipped because AllowForceKillArena is false.");
                        return false;
                    }

                    if (ConfigReader.Current.DryRun)
                    {
                        LogFile.WriteLog("DRY RUN: Would force-kill MTGA process.");
                        return true;
                    }

                    proc.Kill();
                    return true;
                }
            }
            return false;
        }
    }
}
