using Microsoft.AspNetCore.Mvc;
using Oinky.TrainingAppAPI.Models.Request;
using Oinky.TrainingAppAPI.Models.Result;
using Oinky.TrainingAppAPI.Services.Interfaces;
using Oinky.TrainingAppAPI.Utils;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Controllers.API
{
    [Route("api/v{version:apiVersion}/goals")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TrainingGoalsController : ControllerBase
    {
        public TrainingGoalsController(IGoalService goalService)
        {
            m_goalService = goalService;
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "The goal couldnt be added, because the goal was either invalid or not present")]
        public async Task<IActionResult> AddGoal([FromBody] AddGoalRequest requestModel)
        {
            try
            {
                return await m_goalService.AddGoalAsync(requestModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Update the goal with the given ID.
        /// </summary>
        /// <param name="goalID">ID of the goal</param>
        /// <param name="requestModel">The new goal</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{goalID}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "No match or goal with the given ID found")]
        public async Task<IActionResult> UpdateGoal(string goalID, [FromBody] AddGoalRequest requestModel)
        {
            if (!Guid.TryParse(goalID, out Guid goalGUID))
                return BadRequest("GoalID is not a valid guid");
            bool exists = await m_goalService.CheckIfGoalExistsAsync(goalGUID);
            if (!exists)
                return NotFound("Goal not found");
            if (await m_goalService.UpdateGoalAsync(goalGUID, requestModel))
                return Ok();
            return new StatusCodeResult((int)HttpStatusCode.NotModified);
        }

        /// <summary>
        /// Delete the goal with the given ID.
        /// </summary>
        /// <param name="goalID">ID of the goal</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{goalID}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "No match or goal with the given ID found")]
        public async Task<IActionResult> DeleteGoal(string goalID)
        {
            if (!Guid.TryParse(goalID, out Guid goalGUID))
                return BadRequest("GoalID is not a valid guid");
            bool exists = await m_goalService.CheckIfGoalExistsAsync(goalGUID);
            if (!exists)
                return NotFound("Goal not found");
            if (await m_goalService.DeleteGoalAsync(goalGUID))
                return Ok();
            return new StatusCodeResult((int)HttpStatusCode.NotModified);
        }


        /// <summary>
        /// Get the goal with a specific ID.
        /// </summary>
        /// <param name="goalID">ID of the goal</param>
        /// <returns>The requested goal connected to the ID</returns>
        [HttpGet]
        [Route("{goalID}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<ExtendedGoalDTO>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "No match or goal with the given ID found")]
        public async Task<IActionResult> GetGoal(string goalID)
        {
            if (!Guid.TryParse(goalID, out Guid goalGUID))
                return BadRequest("GoalID is not a valid guid");
            var result = await m_goalService.GetGoalAsync(goalGUID);
            if (result == null)
                return NotFound("Goal not found");
            return Ok(result);
        }

        /// <summary>
        /// Get the goal result of a specific match.
        /// </summary>
        /// <param name="goalID">ID of the goal to check</param>
        /// <param name="matchID">ID of the match, which should be analysed</param>
        /// <returns>Result of the training goals for the specific match</returns>
        [HttpGet]
        [Route("{goalID}/{matchID}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<GoalResultDTO>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "No match or goal with the given ID found")]
        public async Task<IActionResult> GetGoalResult(string goalID, string matchID)
        {
            if (!Guid.TryParse(goalID, out Guid goalGUID))
                return BadRequest("GoalID is not a valid guid");
            bool exists = await m_goalService.CheckIfGoalExistsAsync(goalGUID);
            if (!exists)
                return NotFound("Goal not found");
            var result = await m_goalService.CalculateGoalAsync(goalGUID, matchID);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Get all available goals
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(GoalOverviewDTO))]
        public async Task<IActionResult> GetGoals()
        {
            var goals = await m_goalService.GetOverviewAsync();
            return goals != null ? Ok(goals) : NotFound();
        }

        /// <summary>
        /// Get a list of all supported parameters
        /// </summary>
        /// <returns>Result of the training goals for the specific match</returns>
        [HttpGet]
        [Route("parameters")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Dictionary<ParameterCategory, List<string>>))]
        public IActionResult GetParameters()
        {
            return Ok(EquationParameterUtils.Parameters);
        }

        private IGoalService m_goalService;
    }
}