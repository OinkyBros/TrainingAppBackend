using Oinky.TrainingAppAPI.Models.Result;

namespace Oinky.TrainingAppAPI.Services.Interfaces
{
    public interface IGoalService
    {
        Task<GoalOverviewDTO> GetOverviewAsync();
        Task<bool> CheckIfGoalExistsAsync(Guid goalGUID);
    }
}
