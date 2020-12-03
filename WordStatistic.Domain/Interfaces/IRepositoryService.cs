using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WordStatistic.Application.Entities;

namespace WordStatistic.Application.Interfaces
{
    public interface IRepositoryService
    {
        public Task AddOrUpdate(Word word, CancellationToken token = default);
        public Task AddOrUpdate(IEnumerable<Word> words, CancellationToken token = default);
        public int GetOccurencesCount(string source);
    }
}
