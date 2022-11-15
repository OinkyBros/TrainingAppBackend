using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Models.Result;

namespace Oinky.TrainingAppAPI.Repositories.Interfaces
{
    public interface IGoalRepo
    {
        Task<List<GoalDB>> GetOverviewAsync();
        Task<GoalDB> GetGoalAsync(Guid goalGUID);
    }
}