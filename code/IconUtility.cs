using System;
using System.Drawing;

namespace LightFileExplorer
{
    internal static class IconUtility
    {
        public static Icon GetExeIcon()
        {
            return Icon.FromHandle(WindowsApi.LoadIcon(WindowsApi.GetModuleHandle(null), new IntPtr(WindowsApi.IDI_APPLICATION)));
        }
    }
}