using IWshRuntimeLibrary;

namespace LightFileExplorer
{
    internal static class ShellLinkUtility
    {
        public static void CreateShellLink(string linkPath, string targetPath)
        {
            var wshShell = new WshShell();

            var wshShortcut = (IWshShortcut)wshShell.CreateShortcut(linkPath);

            wshShortcut.TargetPath = targetPath;

            wshShortcut.Save();
        }
    }
}