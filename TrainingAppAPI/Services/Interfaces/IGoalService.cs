using Oinky.TrainingAppAPI.Models.Result;

namespace Oinky.TrainingAppAPI.Services.Interfaces
{
    public interface IGoalService
    {
        Task<GoalResultDTO> CalculateGoal(Guid goalID, string matchID);

        Task<bool> CheckIfGoalExistsAsync(Guid goalID);

        Task<GoalOverviewDTO> GetOverviewAsync();
    }
}