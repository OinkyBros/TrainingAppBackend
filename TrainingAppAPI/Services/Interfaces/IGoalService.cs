using Microsoft.AspNetCore.Mvc;
using Oinky.TrainingAppAPI.Models.Request;
using Oinky.TrainingAppAPI.Models.Result;

namespace Oinky.TrainingAppAPI.Services.Interfaces
{
    public interface IGoalService
    {
        Task<IActionResult> AddGoalAsync(AddGoalRequest request);

        Task<GoalResultDTO> CalculateGoalAsync(Guid goalID, string matchID);

        Task<bool> CheckIfGoalExistsAsync(Guid goalID);

        Task<GoalOverviewDTO> GetOverviewAsync();
    }
}