using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WordStatistic.Application.Entities;

namespace WordStatistic.Application.Interfaces
{
    public interface IRepositoryService
    {
        /// <summary>
        /// Add a new word or update the count of appearance of an existing word in the store
        /// </summary>
        /// <seealso cref="IRepositoryService.AddOrUpdate(IEnumerable{Word}, CancellationToken)"/>
        public Task AddOrUpdate(Word word, CancellationToken token = default);

        /// <summary>
        /// Add or update the count of appearance of words in the store
        /// </summary>
        /// <seealso cref="IRepositoryService.AddOrUpdate(Word, CancellationToken)"/>
        public Task AddOrUpdate(IEnumerable<Word> words, CancellationToken token = default);

        /// <summary>
        /// Count of occurences the word, during the API working
        /// </summary>
        public int GetOccurencesCount(string source);
    }
}
