using System;
using System.IO;
using System.Text.RegularExpressions;

namespace LightFileExplorer
{
    /*
     * A note on junctions:
     *
     * When copying items it is expected that junctions are followed (i.e. dereferenced, with target files and folders copied to the destination) and therefore not preserved as junctions.
     * When moving or deleting items it is expected that junctions are moved or deleted as junctions and not followed.
     */

    internal static class FileUtility
    {
        private static readonly Regex ReservedNameRegex = new Regex("^(AUX|COM[1-9]|CON|LPT[1-9]|NUL|PRN)$", RegexOptions.IgnoreCase);

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

        public static void CopyPath(string source, string destination, FileAttributes sourceAttributes)
        {
            if (sourceAttributes.HasFlag(FileAttributes.Directory)) CopyDirectory(source, destination); else CopyFile(source, destination);
        }

        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public static void DeleteDirectory(string path, FileAttributes attributes)
        {
            if (!attributes.HasFlag(FileAttributes.ReparsePoint))
            {
                var findHandle = WindowsApi.FindFirstFile(path + @"\*.*", out WindowsApi.WIN32_FIND_DATA findData);

                if (findHandle == WindowsApi.INVALID_HANDLE_VALUE)
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

                            if (findData.dwFileAttributes.HasFlag(FileAttributes.Directory))
                            {
                                DeleteDirectory(filePath, findData.dwFileAttributes);
                            }
                            else
                            {
                                DeleteFile(filePath, findData.dwFileAttributes);
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

            if (attributes.HasFlag(FileAttributes.ReadOnly))
            {
                File.SetAttributes(path, attributes & ~FileAttributes.ReadOnly);
            }

            Directory.Delete(path, false);
        }

        public static void DeleteFile(string path, FileAttributes attributes)
        {
            if (attributes.HasFlag(FileAttributes.ReadOnly))
            {
                File.SetAttributes(path, FileAttributes.Normal);
            }

            File.Delete(path);
        }

        public static void DeletePath(string path, FileAttributes attributes)
        {
            if (attributes.HasFlag(FileAttributes.Directory)) DeleteDirectory(path, attributes); else DeleteFile(path, attributes);
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
            return ValidNameRegex.IsMatch(name) && !ReservedNameRegex.IsMatch(name);
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

        public static bool ScanMultipleItems(string path, Action<string, DateTime, FileAttributes> folderAction, Action<string, ulong, DateTime, FileAttributes> fileAction, bool ignoreAccessExceptions = false)
        {
            var findHandle = WindowsApi.FindFirstFile(path + @"\*.*", out WindowsApi.WIN32_FIND_DATA findData);

            if (findHandle == WindowsApi.INVALID_HANDLE_VALUE)
            {
                if (ignoreAccessExceptions) return false; else throw new Exception($"Cannot access the \"{path}\" path.");
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

            return true;
        }

        public static bool ScanSingleItem(string path, Action<string, DateTime, FileAttributes> folderAction, Action<string, ulong, DateTime, FileAttributes> fileAction, bool ignoreAccessExceptions = true)
        {
            var findHandle = WindowsApi.FindFirstFile(path, out WindowsApi.WIN32_FIND_DATA findData);

            if (findHandle == WindowsApi.INVALID_HANDLE_VALUE)
            {
                if (ignoreAccessExceptions) return false; else throw new Exception($"Cannot access the \"{path}\" path.");
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

            return true;
        }

        public static void ValidateName(string name)
        {
            if (!IsValidName(name))
            {
                throw new Exception("A name cannot be a Windows reserved name and cannot contain any of these characters: \\ / : * ? < > |");
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