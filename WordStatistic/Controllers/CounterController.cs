using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WordStatistic.Application.Interfaces;

namespace WordStatistic.Controllers
{
    [ApiController]
    [Route("api/counter")]
    public class CounterController : ControllerBase
    {
        private readonly IWordsService _wordsService;
        private readonly IRepositoryService _repoService;

        public CounterController(IWordsService wordsService, IRepositoryService repoService)
        {
            _wordsService = wordsService;
            _repoService = repoService;
        }

        /// <summary>
        /// This API-method convert input string into the set of words and its occurences in the input string.
        /// </summary>
        /// <param name="source">Input</param>
        /// <returns>JSON array</returns>
        [HttpGet]
        public async Task<IActionResult> GetCountResult([FromQuery] string source)
        {
            try
            {
                var result = await _wordsService.Restruct(source);
                await _repoService.AddOrUpdate(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
