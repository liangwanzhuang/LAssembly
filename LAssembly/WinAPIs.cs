using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MR.LTools
{
    public static class WinAPIs
    {
        internal delegate int HookProc(int nCode, System.IntPtr wParam, System.IntPtr lParam);
        private const int WM_SYSCOMMAND = 274;

        private const int SC_MOVE = 61456;

        private const int HTCLIENT = 2;

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern int GetVolumeInformation(string lpRootPathName, string lpVolumeNameBuffer, int nVolumeNameSize, ref int lpVolumeSerialNumber, int lpMaximumComponentLength, int lpFileSystemFlags, string lpFileSystemNameBuffer, int nFileSystemNameSize);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetClassName(System.IntPtr hWnd, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)] [System.Runtime.InteropServices.Out] System.Text.StringBuilder strClassName, int maxLength);

        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern System.IntPtr GetWindowDC(System.IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern int ReleaseDC(System.IntPtr hWnd, System.IntPtr hDC);

        public static string GetDriverNumber(string driver)
        {
            int num = 0;
            WinAPIs.GetVolumeInformation(driver + ":\\", null, 256, ref num, 0, 0, null, 256);
            return num.ToString("X");
        }

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int PostMessage(System.IntPtr hWnd, int msg, int wP, int lP);

        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern int SendMessage(System.IntPtr hWnd, int msg, int wParam, int lParam);

        public static bool SetHotKey(System.Windows.Forms.Form form, string key)
        {
            int num = 4;
            int msg = 50;
            int wP = num * 256 + (int)key[0];
            int num2 = WinAPIs.PostMessage(form.Handle, msg, wP, 0);
            return num2 == 1;
        }

        [System.Runtime.InteropServices.DllImport("user32")]
        internal static extern System.IntPtr SetWindowsHookEx(int nCode, HookProc func, System.IntPtr instance, int threadID);

        [System.Runtime.InteropServices.DllImport("user32")]
        internal static extern int CallNextHookEx(System.IntPtr hook, int code, System.IntPtr wParam, System.IntPtr lParam);

        [System.Runtime.InteropServices.DllImport("user32")]
        internal static extern int UnhookWindowsHookEx(System.IntPtr hook);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        internal static extern bool ReleaseCapture();

        public static void control_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            System.Windows.Forms.Control control = (System.Windows.Forms.Control)sender;
            System.IntPtr handle = control.FindForm().Handle;
            WinAPIs.ReleaseCapture();
            WinAPIs.SendMessage(handle, 274, 61458, 0);
        }

        public static System.Drawing.Point GetMousePoint(System.IntPtr lParam)
        {
            return (System.Drawing.Point)System.Runtime.InteropServices.Marshal.PtrToStructure(lParam, typeof(System.Drawing.Point));
        }

        public static System.Windows.Forms.Keys GetKeys(System.IntPtr lParam)
        {
            return (System.Windows.Forms.Keys)((int)System.Runtime.InteropServices.Marshal.PtrToStructure(lParam, typeof(int)));
        }
    }
}
