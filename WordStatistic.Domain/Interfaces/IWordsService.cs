using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WordStatistic.Application.Entities;

namespace WordStatistic.Application.Interfaces
{
    public interface IWordsService
    {
        /// <summary>
        /// Convert string to the IEnumerable of words and count of their occurences.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="searchInWords">If true, count of occurences also includes subwords.
        /// <para>Example - true: test testA => test:2, testA:1</para>
        /// <para>Example - false: test testA => test:1, testA:1</para> 
        /// </param>
        /// <param name="token">Token for cancel the operation</param>
        /// <returns>IEnumerable<Word></returns>
        public Task<IEnumerable<Word>> Restruct(string source, bool searchInWords = false, CancellationToken token = default);
    }
}
