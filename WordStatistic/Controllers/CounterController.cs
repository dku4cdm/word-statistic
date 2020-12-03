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
