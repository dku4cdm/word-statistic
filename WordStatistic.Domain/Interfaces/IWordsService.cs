using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WordStatistic.Application.Entities;

namespace WordStatistic.Application.Interfaces
{
    public interface IWordsService
    {
        public Task<IEnumerable<Word>> Restruct(string source, CancellationToken token = default);
    }
}
