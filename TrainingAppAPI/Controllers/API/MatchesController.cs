using Microsoft.AspNetCore.Mvc;
using Oinky.TrainingAppAPI.Models.Result;
using Oinky.TrainingAppAPI.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Oinky.TrainingAppAPI.Controllers.API
{
    [Route("api/v{version:apiVersion}/matches")]
    [ApiController]
    [ApiVersion("1.0")]
    public class MatchesController : ControllerBase
    {
        public MatchesController(IConfiguration configuration, IMatchService matchesService)
        {
            m_matchesService = matchesService;
        }

        /// <summary>
        /// Get a list of Matches.
        /// </summary>
        /// <param name="limit">Limits the number of results</param>
        /// <param name="summonername">Filter the matches for a specific summoner</param>
        /// <param name="from">Filter the matches newer than the unix timestamp</param>
        /// <param name="to">Filter the matches older than the unix timestamp </param>
        /// <returns>A set of <see cref="MatchResultDTO"/></returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<MatchResultDTO>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "No matches for the given parameters found.")]
        public async Task<IActionResult> GetMachtes(int limit = 20, string summonername = null, long? from = null, long? to = null)
        {
            List<MatchResultDTO> matches = await m_matchesService.GetMatchesAsync(limit, summonername, from, to);
            return matches != null ? Ok(matches) : NotFound();
        }

        private IMatchService m_matchesService;
    }
}