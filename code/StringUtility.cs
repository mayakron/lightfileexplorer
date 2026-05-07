namespace LightFileExplorer
{
    internal static class StringUtility
    {
        public static string EllipsisInTheMiddle(string text, int useIfLongerThan)
        {
            var textLength = text.Length;

            if (textLength > useIfLongerThan)
            {
                var halfLength = useIfLongerThan / 2;

                return $"{text.Substring(0, halfLength)} [...] {text.Substring(textLength - halfLength, halfLength)}";
            }

            return text;
        }
    }
}