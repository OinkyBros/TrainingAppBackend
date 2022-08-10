using Oinky.TrainingAppAPI.Models.Result;
using Oinky.TrainingAppAPI.Repositories.Interfaces;

namespace Oinky.TrainingAppAPI.Repositories
{
    public class GoalFakeRepo : IGoalRepo
    {
        public Task<GoalOverviewDTO> GetOverviewAsync()
        {
            return Task.FromResult(m_goalOverview);
        }

        public Task<SingleGoalDTO> GetGoalAsync(Guid goalGUID)
        {
            SingleGoalDTO goalDTO = m_goalOverview.DefaultGoals.Where(g => g.GoalID == goalGUID).FirstOrDefault();
            if(goalDTO == null)
                goalDTO = m_goalOverview.CustomGoals.Where(g => g.GoalID == goalGUID).FirstOrDefault();
            return Task.FromResult(goalDTO);
        }

        private static GoalOverviewDTO m_goalOverview = new GoalOverviewDTO()
        {
            DefaultGoals = new List<SingleGoalDTO>()
            {
                new SingleGoalDTO()
                {
                    DisplayName = "Visionscore",
                    GoalID = Guid.Parse("12770df3-2c59-4e79-88e8-8f07ab3e9417")
                }
            }
        };
    }
}