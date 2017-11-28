using System;
using System.Linq;
using System.Collections.Generic;

namespace OpenCVManager.Extensions
{
    public static class StringExtensions
    {
        public static List<string> ToLines(this string text,
            StringSplitOptions options = StringSplitOptions.None) =>
            text.Split(new[] { "\r\n", "\r", "\n" }, options).ToList();
    }
}
