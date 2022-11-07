using Dapper;
using Microsoft.Data.SqlClient;
using Oinky.TrainingAppAPI.Models.Configuration;
using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Repositories.Interfaces;

namespace Oinky.TrainingAppAPI.Repositories.MSSQL
{
    public class SummonerMSSQLRepo : ISummonerRepo
    {
        public SummonerMSSQLRepo(ILogger<SummonerMSSQLRepo> logger, IConfiguration config)
        {
            m_settings = config.GetSection("DBSettings").Get<DBSettings>();
            m_connectionString = m_settings.ConnectionString;
            m_logger = logger;
        }

        public async Task<bool> AddSummonerAsync(SummonerDB summoner)
        {
            string sql = @"INSERT INTO [dbo].[Summoner](DisplayName, PUUID, SummonerLevel, RevisionDate, ProfileIconId, LastUpdate)
                            VALUES (@DisplayName, @PUUID, @SummonerLevel, @RevisionDate, @ProfileIconId, @LastUpdate)";
            using (SqlConnection connection = new SqlConnection(m_connectionString))
            {
                try
                {
                    connection.Open();
                    return await connection.ExecuteAsync(sql, summoner) == 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Not able to register: " + ex.Message);
                    return false;
                }
            }
        }

        public async Task<SummonerDB> GetSummonerAsync(string puuid)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(m_connectionString))
                {
                    connection.Open();
                    return await connection.QueryFirstOrDefaultAsync<SummonerDB>("SELECT * FROM [dbo].[Summoner] WHERE [PUUID]=@PUUID;", new { PUUID = puuid });
                }
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<List<SummonerDB>> GetSummonersAsync()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(m_settings.ConnectionString))
                {
                    connection.Open();
                    List<SummonerDB> result = (await connection.QueryAsync<SummonerDB>("SELECT * FROM [dbo].[Summoner]")).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateUserAsync(SummonerDB summoner)
        {
            try
            {

                if (await GetSummonerAsync(summoner.PUUID) == null)
                    return false;

                using (SqlConnection connection = new SqlConnection(m_settings.ConnectionString))
                {
                    connection.Open();
                    return await connection.ExecuteAsync(@"UPDATE [dbo].[Summoner] SET [LastUpdate] = @LastUpdate WHERE [PUUID]=@PUUID",
                        new
                        {
                            PUUID = summoner.PUUID,
                            LastUpdate = summoner.LastUpdate
                        }) == 1;
                }
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex, ex.Message);
                return false;
            }
        }

        private string m_connectionString;
        private ILogger<SummonerMSSQLRepo> m_logger;
        private DBSettings m_settings;
    }
}