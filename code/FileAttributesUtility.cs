using System.IO;

namespace LightFileExplorer
{
    internal static class FileAttributesUtility
    {
        public static string ToString(FileAttributes fileAttributes)
        {
            var chars = new char[6];

            int charIndex = 0;

            if (fileAttributes.HasFlag(FileAttributes.Archive)) chars[charIndex++] = 'A';
            if (fileAttributes.HasFlag(FileAttributes.ReadOnly)) chars[charIndex++] = 'R';
            if (fileAttributes.HasFlag(FileAttributes.Hidden)) chars[charIndex++] = 'H';
            if (fileAttributes.HasFlag(FileAttributes.System)) chars[charIndex++] = 'S';
            if (fileAttributes.HasFlag(FileAttributes.Compressed)) chars[charIndex++] = 'C';
            if (fileAttributes.HasFlag(FileAttributes.Encrypted)) chars[charIndex++] = 'E';

            return new string(chars, 0, charIndex);
        }
    }
}