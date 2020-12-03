using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WordStatistic.Application.Entities;
using WordStatistic.Application.Exceptions;
using WordStatistic.Application.Interfaces;
using WordStatistic.Domain.Entities;

namespace WordStatistic.Application.Services
{
    public class RepositoryService : IRepositoryService
    {

        public int GetOccurencesCount(string source)
        {
            return source != null && Memory.Data.TryGetValue(source.ToLower(), out int count)
                ? count : 0;
        }

        public async Task AddOrUpdate(Word word, CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("Operation was cancelled by token");
                return;
            }
            if (word.Count < 1)
            {
                throw new DataException("Count cannot be less than 1");
            }
            if (word.Text == null)
            {
                throw new DataException("Text cannot be null");
            }
            if (Memory.Data.ContainsKey(word.Text))
            {
                Memory.Data[word.Text] += word.Count;
            }
            else
            {
                Memory.Data.Add(word.Text, word.Count);
            }
        }

        public async Task AddOrUpdate(IEnumerable<Word> words, CancellationToken token = default)
        {
            foreach (var word in words)
            {
                try
                {
                    await AddOrUpdate(word, token);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}: {ex.StackTrace}");
                }
            }
        }
    }
}
