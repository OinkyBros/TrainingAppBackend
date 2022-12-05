using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Repositories.Interfaces;

namespace Oinky.TrainingAppAPI.Repositories
{
    public class GoalFakeRepo : IGoalRepo
    {
        public Task<bool> AddGoalAsync(GoalDB goalDB)
        {
            m_goals.Add(goalDB.GoalID, goalDB);
            return Task.FromResult(true);
        }

        public Task<bool> DeleteGoalAsync(Guid goalID)
        {
            if (!m_goals.ContainsKey(goalID))
                return Task.FromResult(false);
            return Task.FromResult(m_goals.Remove(goalID));
        }

        public Task<GoalDB> GetGoalAsync(Guid goalID)
        {
            GoalDB goalDB = null;
            if (m_goals.TryGetValue(goalID, out goalDB))
                return Task.FromResult(goalDB);
            return null;
        }

        public Task<List<GoalDB>> GetOverviewAsync()
        {
            return Task.FromResult(m_goals.Values.ToList());
        }

        public Task<bool> UpdateGoalAsync(GoalDB goalDB)
        {
            if (!m_goals.ContainsKey(goalDB.GoalID))
                return Task.FromResult(false);
            m_goals[goalDB.GoalID] = goalDB;
            return Task.FromResult(true);
        }

        private static Dictionary<Guid, GoalDB> m_goals = new Dictionary<Guid, GoalDB>()
        {
            {
                Guid.Parse("12770df3-2c59-4e79-88e8-8f07ab3e9417"),
                new GoalDB()
                {
                    GoalID =  Guid.Parse("12770df3-2c59-4e79-88e8-8f07ab3e9417"),
                    DisplayName = "Visionscore",
                    BotGoal = "{PARTICIPANT:VISIONSCORE}/({MATCH:DURATION}/60)",
                    JungleGoal = "{PARTICIPANT:VISIONSCORE}/(({MATCH:DURATION}/60)*1,5)",
                    MidGoal = "{PARTICIPANT:VISIONSCORE}/({MATCH:DURATION}/60)",
                    SuppGoal = "{PARTICIPANT:VISIONSCORE}/(({MATCH:DURATION}/60)*2)",
                    TopGoal = "{PARTICIPANT:VISIONSCORE}/({MATCH:DURATION}/60)",
                }
            }
        };
    }
}