namespace LightFileExplorer
{
    internal static class StringUtility
    {
        public static string EllipsisInTheMiddle(string text, int maxLength)
        {
            var textLength = text.Length;

            if (textLength > maxLength)
            {
                var halfLength = maxLength / 2;

                return $"{text.Substring(0, halfLength)} [...] {text.Substring(textLength - halfLength, halfLength)}";
            }

            return text;
        }
    }
}