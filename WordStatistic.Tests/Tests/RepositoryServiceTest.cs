using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WordStatistic.Application.Entities;
using WordStatistic.Application.Exceptions;
using WordStatistic.Application.Interfaces;
using WordStatistic.Application.Services;
using WordStatistic.Domain.Entities;
using Xunit;

namespace WordStatistic.Tests.Tests
{
    public class RepositoryServiceTest
    {
        protected readonly IRepositoryService _repoService;
        public RepositoryServiceTest()
        {
            _repoService = new RepositoryService();
            Memory.Data = new Dictionary<string, int>
            {
                { "test", 1 },
                { "like", 3 },
                { "Bike", 2 },
                { "check", 5 },
                { "night34", 6 },
                { "hotel", 8 },
                { "California", 1 },
            };
        }

        [Fact]
        public async Task GetOccurencesTest()
        {
            var source = "test";

            var result = _repoService.GetOccurencesCount(source);

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetOccurencesPartOfWordTest()
        {
            var source = "night";

            var result = _repoService.GetOccurencesCount(source);

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task GetOccurencesCapslockTest()
        {
            var source = "HOTEL";

            var result = _repoService.GetOccurencesCount(source);

            Assert.Equal(8, result);
        }

        [Fact]
        public async Task GetOccurencesNullTest()
        {
            var result = _repoService.GetOccurencesCount(null);

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task GetOccurencesEmptyTest()
        {
            var source = "";

            var result = _repoService.GetOccurencesCount(source);

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task GetOccurencesNotExistedTest()
        {
            var source = "qwerty";

            var result = _repoService.GetOccurencesCount(source);

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task AddOrUpdateTest()
        {
            var source = new Word("qwerty", 3);

            await _repoService.AddOrUpdate(source);

            Assert.True(Memory.Data.ContainsKey(source.Text));
        }

        [Fact]
        public async Task AddOrUpdateCancelOperationTest()
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            var source = new Word("qwerty", 3);

            cancelTokenSource.Cancel();
            await _repoService.AddOrUpdate(source, token);

            Assert.False(Memory.Data.ContainsKey(source.Text));
        }

        [Fact]
        public async Task AddOrUpdateEnumerableTest()
        {
            var source = new HashSet<Word>
            {
                new Word("one", 1),
                new Word("two", 2),
                new Word("three", 3),
                new Word("check", 3),
            };

            await _repoService.AddOrUpdate(source, CancellationToken.None);

            var checkCount = Memory.Data["check"];
            Assert.Equal(8, checkCount);
            Assert.Equal(10, Memory.Data.Count);
        }

        [Fact]
        public async Task AddOrUpdateWordLessOneCountTest()
        {
            var source = new Word("one", -1);

            await Assert.ThrowsAsync<DataException>(async ()
                => await _repoService.AddOrUpdate(source, CancellationToken.None));
        }

        [Fact]
        public async Task AddOrUpdateWordLessZeroCountTest()
        {
            var source = new HashSet<Word>
            {
                new Word("one", 0),
                new Word("two", 2),
                new Word("three", -3),
                new Word(null, 4),
                new Word("five", 5),
                new Word("test", -1),
            };

            await _repoService.AddOrUpdate(source, CancellationToken.None);


            var test = Memory.Data["test"];
            Assert.False(Memory.Data.ContainsKey("one"));
            Assert.True(Memory.Data.ContainsKey("two"));
            Assert.False(Memory.Data.ContainsKey("three"));
            Assert.True(Memory.Data.ContainsKey("five"));
            Assert.True(Memory.Data.ContainsKey("test"));
            Assert.Equal(1, test);
        }
    }
}
