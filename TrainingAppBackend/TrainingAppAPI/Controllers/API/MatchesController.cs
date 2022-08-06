using Microsoft.AspNetCore.Mvc;
using Oinky.TrainingAppAPI.Models.Result;
using Oinky.TrainingAppAPI.Services.Interfaces;

namespace Oinky.TrainingAppAPI.Controllers.API
{
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    [ApiVersion("1.0")]
    public class MatchesController : ControllerBase
    {
        public MatchesController(IConfiguration configuration, IMatchService matchesService)
        {
            m_matchesService = matchesService;
        }

        [HttpGet]
        [Route("matches")]
        public async Task<IActionResult> GetMachtes()
        {
            List<Match> matches = await m_matchesService.GetMatchesAsync();
            return matches != null ? Ok(matches) : NotFound();
        }

        private IMatchService m_matchesService;
    }
}