namespace LightFileExplorer
{
    internal static class StringUtility
    {
        public static string EllipsisInTheMiddle(string text, int useEllipsisIfLongerThan)
        {
            var textLength = text.Length;

            if (textLength > useEllipsisIfLongerThan)
            {
                var halfLength = useEllipsisIfLongerThan / 2;

                return $"{text.Substring(0, halfLength)} [...] {text.Substring(textLength - halfLength, halfLength)}";
            }

            return text;
        }
    }
}