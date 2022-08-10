using Oinky.TrainingAppAPI.Models.Result;

namespace Oinky.TrainingAppAPI.Repositories.Interfaces
{
    public interface IGoalRepo
    {
        Task<GoalOverviewDTO> GetOverviewAsync();
        Task<SingleGoalDTO> GetGoalAsync(Guid goalGUID);
    }
}