using Oinky.TrainingAppAPI.Models.DB;

namespace Oinky.TrainingAppAPI.Repositories.Interfaces
{
    public interface IGoalRepo
    {
        Task<bool> AddGoalAsync(GoalDB goalDB);

        Task<bool> DeleteGoalAsync(Guid goalID);

        Task<GoalDB> GetGoalAsync(Guid goalID);

        Task<List<GoalDB>> GetOverviewAsync();

        Task<bool> UpdateGoalAsync(GoalDB goalDB);
    }
}