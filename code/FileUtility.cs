using System;
using System.IO;
using System.Text.RegularExpressions;

namespace LightFileExplorer
{
    internal static class FileUtility
    {
        private static readonly Regex ValidNameRegex = new Regex("^[^\\\\/:*?\"<>|]+$");

        private static readonly Regex ValidPathRegex = new Regex("^[A-Za-z]:\\\\[^:*?\"<>|]*$");

        public static void CopyDirectory(string source, string destination)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(source, destination, true);
        }

        public static void CopyFile(string source, string destination)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(source, destination, true);
        }

        public static void CopyPath(string source, string destination)
        {
            if (File.GetAttributes(source).HasFlag(FileAttributes.Directory)) CopyDirectory(source, destination); else CopyFile(source, destination);
        }

        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public static void DeleteDirectory(string path)
        {
            var findHandle = WindowsApi.FindFirstFile(path + @"\*.*", out WindowsApi.WIN32_FIND_DATA findData);

            if (findHandle.ToInt64() <= 0)
            {
                throw new Exception($"Cannot access the \"{path}\" path.");
            }

            try
            {
                do
                {
                    if ((findData.cFileName != ".") && (findData.cFileName != ".."))
                    {
                        var filePath = Path.Combine(path, findData.cFileName);

                        if (findData.dwFileAttributes.HasFlag(FileAttributes.ReadOnly))
                        {
                            File.SetAttributes(filePath, FileAttributes.Normal);
                        }

                        if (findData.dwFileAttributes.HasFlag(FileAttributes.Directory))
                        {
                            DeleteDirectory(filePath);
                        }
                        else
                        {
                            File.Delete(filePath);
                        }
                    }
                }
                while (WindowsApi.FindNextFile(findHandle, out findData));
            }
            finally
            {
                WindowsApi.FindClose(findHandle);
            }

            Directory.Delete(path, false);
        }

        public static void DeleteFile(string path)
        {
            if (File.GetAttributes(path).HasFlag(FileAttributes.ReadOnly))
            {
                File.SetAttributes(path, FileAttributes.Normal);
            }

            File.Delete(path);
        }

        public static void DeletePath(string path)
        {
            if (File.GetAttributes(path).HasFlag(FileAttributes.Directory)) DeleteDirectory(path); else DeleteFile(path);
        }

        public static bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public static FileAttributes GetAttributes(string path)
        {
            return File.GetAttributes(path);
        }

        public static string[] GetLogicalDrives()
        {
            return Directory.GetLogicalDrives();
        }

        public static bool IsValidName(string name)
        {
            return ValidNameRegex.IsMatch(name);
        }

        public static bool IsValidPath(string path)
        {
            return ValidPathRegex.IsMatch(path);
        }

        public static void MoveDirectory(string source, string destination)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory(source, destination, true);
        }

        public static void MoveFile(string source, string destination)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.MoveFile(source, destination, true);
        }

        public static void MovePath(string source, string destination)
        {
            if (File.GetAttributes(source).HasFlag(FileAttributes.Directory)) MoveDirectory(source, destination); else MoveFile(source, destination);
        }

        public static bool PathExists(string path)
        {
            return WindowsApi.PathFileExists(path);
        }

        public static void RenamePath(string source, string destination)
        {
            Microsoft.VisualBasic.FileSystem.Rename(source, destination);
        }

        public static void ScanMany(string path, Action<string, DateTime, FileAttributes> folderAction, Action<string, ulong, DateTime, FileAttributes> fileAction)
        {
            var findHandle = WindowsApi.FindFirstFile(path + @"\*.*", out WindowsApi.WIN32_FIND_DATA findData);

            if (findHandle.ToInt64() <= 0)
            {
                throw new Exception($"Cannot access the \"{path}\" path.");
            }

            try
            {
                do
                {
                    if ((findData.cFileName != ".") && (findData.cFileName != ".."))
                    {
                        var lastWriteTime = DateTime.FromFileTime((long)findData.ftLastWriteTimeLo + ((long)findData.ftLastWriteTimeHi << 32));

                        if (findData.dwFileAttributes.HasFlag(FileAttributes.Directory))
                        {
                            folderAction(findData.cFileName, lastWriteTime, findData.dwFileAttributes);
                        }
                        else
                        {
                            ulong size = (ulong)findData.nFileSizeLo + ((ulong)findData.nFileSizeHi << 32);

                            fileAction(findData.cFileName, size, lastWriteTime, findData.dwFileAttributes);
                        }
                    }
                }
                while (WindowsApi.FindNextFile(findHandle, out findData));
            }
            finally
            {
                WindowsApi.FindClose(findHandle);
            }
        }

        public static void ScanSingle(string path, Action<string, DateTime, FileAttributes> folderAction, Action<string, ulong, DateTime, FileAttributes> fileAction)
        {
            var findHandle = WindowsApi.FindFirstFile(path, out WindowsApi.WIN32_FIND_DATA findData);

            if (findHandle.ToInt64() <= 0)
            {
                throw new Exception($"Cannot access the \"{path}\" path.");
            }

            try
            {
                var lastWriteTime = DateTime.FromFileTime((long)findData.ftLastWriteTimeLo + ((long)findData.ftLastWriteTimeHi << 32));

                if (findData.dwFileAttributes.HasFlag(FileAttributes.Directory))
                {
                    folderAction(findData.cFileName, lastWriteTime, findData.dwFileAttributes);
                }
                else
                {
                    ulong size = (ulong)findData.nFileSizeLo + ((ulong)findData.nFileSizeHi << 32);

                    fileAction(findData.cFileName, size, lastWriteTime, findData.dwFileAttributes);
                }
            }
            finally
            {
                WindowsApi.FindClose(findHandle);
            }
        }

        public static void ValidateName(string name)
        {
            if (!IsValidName(name))
            {
                throw new Exception("A name cannot contain any of these characters: \\ / : * ? < > |");
            }
        }

        public static void ValidatePath(string path)
        {
            if (!IsValidPath(path))
            {
                throw new Exception($"The \"{path}\" path is not valid.");
            }
        }
    }
}