using System;
using System.Windows.Forms;

namespace LightFileExplorer
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            if (args != null)
            {
                if (args.Length > 0)
                {
                    var path = args[0];

                    if (FileUtility.DirectoryExists(path))
                    {
                        try
                        {
                            Environment.CurrentDirectory = path;
                        }
                        catch
                        {
                        }
                    }
                }
            }

            Application.EnableVisualStyles(); Application.SetCompatibleTextRenderingDefault(false); Application.Run(new MainWindow());
        }
    }
}