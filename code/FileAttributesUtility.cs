using System.IO;

namespace LightFileExplorer
{
    internal static class FileAttributesUtility
    {
        public static string ToString(FileAttributes fileAttributes)
        {
            return (fileAttributes.HasFlag(FileAttributes.Archive) ? "A" : null) +
                   (fileAttributes.HasFlag(FileAttributes.ReadOnly) ? "R" : null) +
                   (fileAttributes.HasFlag(FileAttributes.Hidden) ? "H" : null) +
                   (fileAttributes.HasFlag(FileAttributes.System) ? "S" : null) +
                   (fileAttributes.HasFlag(FileAttributes.Compressed) ? "C" : null) +
                   (fileAttributes.HasFlag(FileAttributes.Encrypted) ? "E" : null);
        }
    }
}