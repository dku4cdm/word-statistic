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

        /// <summary>
        /// This API-method returns count of occurences the source 
        /// which had entered before, during the API work.
        /// </summary>
        /// <param name="source">Input</param>
        /// <returns>Number</returns>
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
