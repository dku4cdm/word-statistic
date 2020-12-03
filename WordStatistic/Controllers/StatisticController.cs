using Microsoft.AspNetCore.Mvc;
using System;
using WordStatistic.Application.Interfaces;

namespace WordStatistic.Controllers
{
    [ApiController]
    [Route("api/statistic")]
    public class StatisticController : ControllerBase
    {
        private readonly IRepositoryService _repoService;

        public StatisticController(IRepositoryService repoService)
        {
            _repoService = repoService;
        }

        [HttpGet]
        public IActionResult GetOccurencesCount([FromQuery] string source)
        {
            try
            {
                var result = _repoService.GetOccurencesCount(source);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
