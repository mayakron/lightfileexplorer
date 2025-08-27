using System;
using System.IO;
using System.Runtime.InteropServices;

namespace LightFileExplorer
{
    internal static class WindowsApi
    {
        internal const int IDI_APPLICATION = 32512;

        internal const uint LVM_SETTEXTBKCOLOR = 0x1026;

        [DllImport("Kernel32", SetLastError = true)]
        internal static extern bool FindClose(IntPtr hFindFile);

        [DllImport("Kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr FindFirstFile(string fileName, out WIN32_FIND_DATA findFileData);

        [DllImport("Kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool FindNextFile(IntPtr findFileHandle, out WIN32_FIND_DATA findFileData);

        [DllImport("Kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern uint GetFileAttributes(string fileName);

        [DllImport("Kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string moduleName);

        [DllImport("User32", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr LoadIcon(IntPtr handle, IntPtr iconName);

        [DllImport("ShlwApi", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool PathFileExists(string path);

        [DllImport("User32", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool SendMessage(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("Kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool SetFileAttributes(string fileName, FileAttributes fileAttributes);

        [BestFitMapping(false)]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct WIN32_FIND_DATA
        {
            internal FileAttributes dwFileAttributes;

            internal uint ftCreationTimeLo;

            internal uint ftCreationTimeHi;

            internal uint ftLastAccessTimeLo;

            internal uint ftLastAccessTimeHi;

            internal uint ftLastWriteTimeLo;

            internal uint ftLastWriteTimeHi;

            internal uint nFileSizeHi;

            internal uint nFileSizeLo;

            internal uint dwReserved0;

            internal uint dwReserved1;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            internal string cFileName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            internal string cAlternate;
        }
    }
}