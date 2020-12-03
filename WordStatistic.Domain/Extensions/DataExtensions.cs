using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WordStatistic.Application.Entities;

namespace WordStatistic.Application.Extensions
{
    public static class DataExtensions
    {
        private static char[] Separators { get; set; } = 
            new char[] { ' ', '\r', '\n', '\t', ',', '.', ';', '!', '?', '(', ')', '/', '&', '^', '@', '*', '_', '-', '+', '=', '#' };
        public static IEnumerable<Word> ConvertToResult(this string source)
        {
            return source
                .Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                .Distinct()
                .Select(word => new Word(word, Regex.Matches(source, word, RegexOptions.IgnoreCase).Count));
        }
    }
}
