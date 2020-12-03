using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WordStatistic.Application.Entities;
using WordStatistic.Application.Extensions;
using WordStatistic.Application.Interfaces;

namespace WordStatistic.Application.Services
{
    public class WordsService : IWordsService
    {
        public async Task<IEnumerable<Word>> Restruct(string source, bool searchInWords = false, CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("Operation was cancelled by token");
                return Enumerable.Empty<Word>();
            }

            return source.ToLower().ConvertToResult(searchInWords);
        }
    }
}
