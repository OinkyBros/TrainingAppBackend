using Dapper;
using Microsoft.Data.SqlClient;
using Oinky.TrainingAppAPI.Models.Configuration;
using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Repositories.Interfaces;

namespace Oinky.TrainingAppAPI.Repositories.MSSQL
{
    public class GoalMSSQLRepo : IGoalRepo
    {
        public GoalMSSQLRepo(ILogger<GoalMSSQLRepo> logger, IConfiguration config)
        {
            m_settings = config.GetSection("DBSettings").Get<DBSettings>();
            m_connectionString = m_settings.ConnectionString;
            m_logger = logger;
        }

        public async Task<GoalDB> GetGoalAsync(Guid goalID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(m_connectionString))
                {
                    connection.Open();
                    return (await connection.QueryAsync<GoalDB>("SELECT * FROM [dbo].[Goal] WHERE GoalID=@GoalID", new { GoalID = goalID })).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<List<GoalDB>> GetOverviewAsync()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(m_connectionString))
                {
                    connection.Open();
                    List<GoalDB> result = (await connection.QueryAsync<GoalDB>("SELECT * FROM [dbo].[Goal]")).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> AddGoalAsync(GoalDB goalDB)
        {
            using (SqlConnection connection = new SqlConnection(m_connectionString))
            {
                try
                {
                    string sql = @"INSERT INTO [dbo].[Goal](GoalID, DisplayName, TopGoal, JungleGoal, MidGoal, BotGoal, SuppGoal)
                            VALUES (@GoalID, @DisplayName, @TopGoal, @JungleGoal, @MidGoal, @BotGoal, @SuppGoal)";

                    {
                        connection.Open();
                        SqlTransaction transaction = connection.BeginTransaction();
                        //Add match
                        if (await connection.ExecuteAsync(sql, goalDB, transaction) < 1)
                        {
                            //Cleanup
                            transaction.Rollback();
                            return false;
                        }
                        await transaction.CommitAsync();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    m_logger.LogError(ex, ex.Message);
                    return false;
                }
            }
        }

        private string m_connectionString;
        private ILogger<GoalMSSQLRepo> m_logger;
        private DBSettings m_settings;
    }
}