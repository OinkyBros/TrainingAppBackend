using Oinky.TrainingAppAPI.Models.Result;
using Oinky.TrainingAppAPI.Repositories.Interfaces;
using Oinky.TrainingAppAPI.Services.Interfaces;

namespace Oinky.TrainingAppAPI.Services
{
    public class GoalService : IGoalService
    {
        public GoalService(ILogger<GoalService> logger, IGoalRepo goalRepo, IMatchRepo matchRepo)
        {
            m_goalRepo = goalRepo;
            m_matchRepo = matchRepo;
        }

        public async Task<GoalOverviewDTO> GetOverviewAsync()
        {
            return await m_goalRepo.GetOverviewAsync();
        }

        public async Task<bool> CheckIfGoalExistsAsync(Guid goalGUID)
        {
            SingleGoalDTO goal = await m_goalRepo.GetGoalAsync(goalGUID);
            return goal != null;
        }

        private IGoalRepo m_goalRepo;
        private IMatchRepo m_matchRepo;
    }
}