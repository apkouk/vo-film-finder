using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersioOriginalTester.Classes
{
    public class CinevoStrings
    {
        public static string GetChunk(string text, string from, string to)
        {
            int intFrom = text.IndexOf(from) + from.Length;
            int intTo = text.IndexOf(to);
            int moveChars = intTo - intFrom;
            return text.Substring(intFrom, moveChars);
        }

        public static string GetChunk(string text, string from, string to, string removeFromLast)
        {
            string result = string.Empty;
            int intFrom = text.IndexOf(from) + from.Length;
            int intTo = text.IndexOf(to);
            int moveChars = intTo - intFrom;
            result = text.Substring(intFrom, moveChars);
            result = result.Remove(result.LastIndexOf(removeFromLast));
            return result;
        }
    }
}
