namespace WebInkLibrary.Utils.StringHelper
{
    public class StringHelper
    {
        public static string GetShortString(string fullString, int length)
        {
            return fullString.Length > length ? fullString.Substring(0, length) + "..." : fullString;
        }
    }
}
