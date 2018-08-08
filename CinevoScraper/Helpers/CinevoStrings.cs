using System;
using System.Text.RegularExpressions;

namespace CinevoScraper.Helpers
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

        public static string StripHtml(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        public static string RemoveChars(string lineHtml, char _char)
        {
            string[] words = lineHtml.Split(_char);
            string result = string.Empty;

            for (int i = 0; i < words.Length; i++)
            {
                if (!words[i].Equals(string.Empty))
                {
                    result += words[i] + " ";
                }
            }
            return result;
        }
    }
}