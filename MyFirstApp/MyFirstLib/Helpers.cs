using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace MyFirstLib
{
    public class Helpers
    {
        public static List<string> GetWords(string text)
        {
            //if (String.IsNullOrEmpty(text))
            //    throw new ArgumentNullException(nameof(text));

            _ = text ?? throw new ArgumentNullException(text);

            var r = new Regex(@"\w+");
            var matches = r.Matches(text);
            var list = new List<string>();
            foreach (Match m in matches)
            {
                list.Add(m.Value);
            }
            return list;

            //  Shorter with LINQ
            //  return (matches as IEnumerable<Match>).Select(x => x.Value).ToList();
        }
    }
}
