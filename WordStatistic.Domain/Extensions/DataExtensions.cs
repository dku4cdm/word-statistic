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

        public static IEnumerable<Word> ConvertToResult(this string source, bool searchInWords = false)
        {
            if (searchInWords)
            {
                foreach (var word in source
                    .Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                    .Distinct()
                    .Select(word => new Word(word, Regex.Matches(source, word, RegexOptions.IgnoreCase).Count)))
                {
                    yield return word;
                }
                yield break;
            }

            foreach (var word in source
                .Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                .Distinct()
                .OrderByDescending(x => x.Length))
            {
                var wordIndex = source.IndexOf(word);
                var count = 0;
                while (wordIndex != -1)
                {
                    source = $"{source.Substring(0, wordIndex)}{source.Substring(wordIndex + word.Length)}";
                    wordIndex = source.IndexOf(word);
                    count++;
                }
                yield return new Word(word, count);
            }
        }
    }
}
