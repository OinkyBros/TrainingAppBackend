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

        private string m_connectionString;
        private ILogger<GoalMSSQLRepo> m_logger;
        private DBSettings m_settings;
    }
}