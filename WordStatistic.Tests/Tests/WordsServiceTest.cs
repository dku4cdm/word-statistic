using System.Linq;
using System.Threading.Tasks;
using WordStatistic.Application.Interfaces;
using WordStatistic.Application.Services;
using Xunit;

namespace WordStatistic.Tests.Tests
{
    public class WordsServiceTest
    {
        protected readonly IWordsService _wordsService;

        public WordsServiceTest()
        {
            _wordsService = new WordsService();
        }

        [Fact]
        public async Task RestructStringTest()
        {
            var source = "Test xtest test3 test/";
            
            var result = await _wordsService.Restruct(source);

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task RestructEmptyStringTest()
        {
            var source = "";

            var result = await _wordsService.Restruct(source);

            Assert.Empty(result);
        }

        [Fact]
        public async Task RestructStringWithSignsTest()
        {
            var source = "test#data.qwerty^";

            var result = await _wordsService.Restruct(source);

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task RestructExampleStringTest()
        {
            var source = "Hi! my name is (what?), my name is (who?), my name is Bean";

            var result = await _wordsService.Restruct(source);

            Assert.NotEmpty(result);
            var name = result.FirstOrDefault(x => x.Text == "name");
            var bean = result.FirstOrDefault(x => x.Text == "bean");
            Assert.Equal(3, name.Count);
            Assert.Equal(1, bean.Count);
        }

        [Fact]
        public async Task RestructWithSearchInWordsStringTest()
        {
            var source = "testA test testA testB testABC";

            var result = await _wordsService.Restruct(source, true);

            Assert.NotEmpty(result);
            var test = result.FirstOrDefault(x => x.Text == "test");
            var testA = result.FirstOrDefault(x => x.Text == "testa");
            Assert.Equal(5, test.Count);
            Assert.Equal(3, testA.Count);
        }


        [Fact]
        public async Task RestructWithoutSearchInWordsStringTest()
        {
            var source = "testA test testA testB testABC";

            var result = await _wordsService.Restruct(source, false);

            Assert.NotEmpty(result);
            var test = result.FirstOrDefault(x => x.Text == "test");
            var testA = result.FirstOrDefault(x => x.Text == "testa");
            Assert.Equal(1, test.Count);
            Assert.Equal(2, testA.Count);
        }
    }
}
