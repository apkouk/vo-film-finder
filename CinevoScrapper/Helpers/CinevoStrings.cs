using System;

namespace CinevoScrapper.Helpers
{
    public class CinevoStrings
    {
        public static string GetChunk(string text, string from, string to)
        {
            var intFrom = text.IndexOf(from, StringComparison.Ordinal) + from.Length;
            var intTo = text.IndexOf(to, StringComparison.Ordinal);
            var moveChars = intTo - intFrom;
            return text.Substring(intFrom, moveChars);
        }

        public static string GetChunk(string text, string from, string to, string removeFromLast)
        {
            var intFrom = text.IndexOf(from, StringComparison.Ordinal) + from.Length;
            var intTo = text.IndexOf(to, StringComparison.Ordinal);
            var moveChars = intTo - intFrom;
            var result = text.Substring(intFrom, moveChars);
            result = result.Remove(result.LastIndexOf(removeFromLast, StringComparison.Ordinal));
            return result;
        }
    }
}