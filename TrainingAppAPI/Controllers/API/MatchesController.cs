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
        /// <returns>A set of <see cref="MatchDTO"/></returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<MatchDTO>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "No matches for the given parameters found.")]
        public async Task<IActionResult> GetMatches(int limit = 20, string summonername = null, long? from = null, long? to = null)
        {
            List<MatchDTO> matches = await m_matchesService.GetMatchesAsync(limit, summonername, from, to);
            return matches != null ? Ok(matches) : NotFound();
        }

        /// <summary>
        /// Get a specific Match.
        /// </summary>
        /// <param name="matchID">The id of the requested match</param>
        /// <returns>Details of the specific match</returns>
        [HttpGet]
        [Route("{matchID}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ExtendedMatchDTO))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "No match with the given ID found")]
        public async Task<IActionResult> GetMatch(string matchID)
        {
            ExtendedMatchDTO match = await m_matchesService.GetMatchAsync(matchID);
            return match != null ? Ok(match) : NotFound();
        }

        private IMatchService m_matchesService;
    }
}