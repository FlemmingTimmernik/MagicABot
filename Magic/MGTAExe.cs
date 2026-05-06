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
            string steamPath = @"C:\Program Files (x86)\Steam\steam.exe";
            Process mgta = new Process();

            mgta.StartInfo.FileName = steamPath;
            mgta.StartInfo.Arguments = "-applaunch 2141910";
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
                   proc.Kill();
                    return true;
                }
            }
            return false;
        }
    }
}
