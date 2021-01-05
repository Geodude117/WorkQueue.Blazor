using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace WorkQueue.MVC.Helpers
{
    public static class StringHelper
    {
        private static readonly Regex sWhitespace = new Regex(@"\s+");

        public static string ReplaceWhitespace(string input, string replacement)
        {
            return sWhitespace.Replace(input, replacement);
        }
    }
}


