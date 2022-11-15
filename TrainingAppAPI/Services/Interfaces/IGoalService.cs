using Oinky.TrainingAppAPI.Models.Result;

namespace Oinky.TrainingAppAPI.Services.Interfaces
{
    public interface IGoalService
    {
        Task<GoalResultDTO> CalculateGoal(Guid goalGUID, string matchID);

        Task<bool> CheckIfGoalExistsAsync(Guid goalGUID);

        Task<GoalOverviewDTO> GetOverviewAsync();
    }
}