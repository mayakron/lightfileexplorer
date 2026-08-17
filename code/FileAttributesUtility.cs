using System.IO;

namespace LightFileExplorer
{
    internal static class FileAttributesUtility
    {
        public static string ToString(FileAttributes fileAttributes)
        {
            var chars = new char[6];

            int charIndex = 0;

            if ((fileAttributes & FileAttributes.Archive) != 0) chars[charIndex++] = 'A';
            if ((fileAttributes & FileAttributes.ReadOnly) != 0) chars[charIndex++] = 'R';
            if ((fileAttributes & FileAttributes.Hidden) != 0) chars[charIndex++] = 'H';
            if ((fileAttributes & FileAttributes.System) != 0) chars[charIndex++] = 'S';
            if ((fileAttributes & FileAttributes.Compressed) != 0) chars[charIndex++] = 'C';
            if ((fileAttributes & FileAttributes.Encrypted) != 0) chars[charIndex++] = 'E';

            return new string(chars, 0, charIndex);
        }
    }
}