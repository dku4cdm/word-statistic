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
    }
}
