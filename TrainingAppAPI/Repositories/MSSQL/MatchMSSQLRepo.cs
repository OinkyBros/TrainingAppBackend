using Dapper;
using Microsoft.Data.SqlClient;
using Oinky.TrainingAppAPI.Models.Configuration;
using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Repositories.Interfaces;

namespace Oinky.TrainingAppAPI.Repositories.MSSQL
{
    public class MatchMSSQLRepo : IMatchRepo
    {
        public MatchMSSQLRepo(ILogger<SummonerMSSQLRepo> logger, IConfiguration config)
        {
            m_settings = config.GetSection("DBSettings").Get<DBSettings>();
            m_connectionString = m_settings.ConnectionString;
            m_logger = logger;
        }

        public async Task<bool> AddMatchAsync(MatchDB matchDB)
        {
            SqlTransaction transaction = null;
            using (SqlConnection connection = new SqlConnection(m_connectionString))
            {
                try
                {
                    string sql = @"INSERT INTO [dbo].[Match](MatchID, GameCreation, GameDuration, GameMode, GameStartTimestamp)
                            VALUES (@MatchID, @GameCreation, @GameDuration, @GameMode, @GameStartTimestamp)";

                    {
                        connection.Open();
                        transaction = connection.BeginTransaction();
                        //Add match
                        if (await connection.ExecuteAsync(sql, matchDB, transaction) < 1)
                        {
                            //Cleanup
                            transaction.Rollback();
                            return false;
                        }
                        //Add teams
                        sql = @"INSERT INTO [dbo].[Team](TeamID, MatchID, Bans,
                                                   Barons, ChampionKills, Dragons,
                                                   FirstBaron, FirstBlood, FirstDragon,
                                                   FirstHerald, FirstInhibitors, FirstTower,
                                                   Heralds, IngameID, Inhibitors,
                                                   Towers, Win)
                            VALUES (@TeamID, @MatchID, @Bans,
                                    @Barons, @ChampionKills, @Dragons,
                                    @FirstBaron, @FirstBlood, @FirstDragon,
                                    @FirstHerald, @FirstInhibitors, @FirstTower,
                                    @Heralds, @IngameID, @Inhibitors,
                                    @Towers, @Win)";
                        List<ParticipantDB> participants = new List<ParticipantDB>();
                        foreach (TeamDB teamDB in matchDB.Teams)
                        {
                            if (await connection.ExecuteAsync(sql, teamDB, transaction) < 1)
                            {
                                transaction.Rollback();
                                return false;
                            }
                            participants.AddRange(teamDB.Participants);
                        }

                        sql = @"INSERT INTO [dbo].[Participant](Assists, BaronKills, BasicPings,
                                                   BountyLevel, ChampExperience, ChampionId,
                                                   ChampionName, ChampionTransform, ChampLevel,
                                                   ConsumablesPurchased, DamageDealtToBuildings, DamageDealtToObjectives,
                                                   DamageDealtToTurrets, DamageSelfMitigated, Deaths,
                                                   DetectorWardsPlaced, DoubleKills, DragonKills,
                                                   EligibleForProgression, FirstBloodAssist, FirstBloodKill,
                                                   FirstTowerAssist, FirstTowerKill, GameEndedInEarlySurrender,
                                                   GameEndedInSurrender, GoldEarned, GoldSpent,
                                                   InhibitorKills, InhibitorsLost, InhibitorTakedowns,
                                                   Item0, Item1, Item2,
                                                   Item3, Item4, Item5,
                                                   Item6, ItemsPurchased, KillingSprees,
                                                   Kills, LargestCriticalStrike, LargestKillingSpree,
                                                   LargestMultiKill, LongestTimeSpentLiving, MagicDamageDealt,
                                                   MagicDamageDealtToChampions, MagicDamageTaken, NeutralMinionsKilled,
                                                   NexusKills, NexusLost, NexusTakedowns,
                                                   ObjectivesStolen, ObjectivesStolenAssists, ParticipantId,
                                                   PentaKills, PhysicalDamageDealt, PhysicalDamageDealtToChampions,
                                                   PhysicalDamageTaken, ProfileIcon, Puuid,
                                                   QuadraKills, RiotIdName, RiotIdTagline,
                                                   Role, SightWardsBoughtInGame, Spell1Casts,
                                                   Spell2Casts, Spell3Casts, Spell4Casts,
                                                   Summoner1Casts, Summoner1Id, Summoner2Casts,
                                                   Summoner2Id, SummonerId, SummonerLevel,
                                                   SummonerName, TeamEarlySurrendered, TeamID,
                                                   TimeCCingOthers, TimePlayed, TotalDamageDealt,
                                                   TotalDamageDealtToChampions, TotalDamageShieldedOnTeammates, TotalDamageTaken,
                                                   TotalHeal, TotalHealsOnTeammates, TotalMinionsKilled,
                                                   TotalTimeCCDealt, TotalTimeSpentDead, TotalUnitsHealed,
                                                   TowerKills, TowerTakedowns, TowersLost, TripleKills,
                                                   TrueDamageDealt, TrueDamageDealtToChampions, TrueDamageTaken,
                                                   UnrealKills, VisionScore, VisionWardsBoughtInGame,
                                                   WardsKilled, WardsPlaced)
                            VALUES (@Assists, @BaronKills, @BasicPings,
                                    @BountyLevel, @ChampExperience, @ChampionId,
                                    @ChampionName, @ChampionTransform, @ChampLevel,
                                    @ConsumablesPurchased, @DamageDealtToBuildings, @DamageDealtToObjectives,
                                    @DamageDealtToTurrets, @DamageSelfMitigated, @Deaths,
                                    @DetectorWardsPlaced, @DoubleKills, @DragonKills,
                                    @EligibleForProgression, @FirstBloodAssist, @FirstBloodKill,
                                    @FirstTowerAssist, @FirstTowerKill, @GameEndedInEarlySurrender,
                                    @GameEndedInSurrender, @GoldEarned, @GoldSpent,
                                    @InhibitorKills, @InhibitorsLost, @InhibitorTakedowns,
                                    @Item0, @Item1, @Item2,
                                    @Item3, @Item4, @Item5,
                                    @Item6, @ItemsPurchased, @KillingSprees,
                                    @Kills, @LargestCriticalStrike, @LargestKillingSpree,
                                    @LargestMultiKill, @LongestTimeSpentLiving, @MagicDamageDealt,
                                    @MagicDamageDealtToChampions, @MagicDamageTaken, @NeutralMinionsKilled,
                                    @NexusKills, @NexusLost, @NexusTakedowns,
                                    @ObjectivesStolen, @ObjectivesStolenAssists, @ParticipantId,
                                    @PentaKills, @PhysicalDamageDealt, @PhysicalDamageDealtToChampions,
                                    @PhysicalDamageTaken, @ProfileIcon, @Puuid,
                                    @QuadraKills, @RiotIdName, @RiotIdTagline,
                                    @Role, @SightWardsBoughtInGame, @Spell1Casts,
                                    @Spell2Casts, @Spell3Casts, @Spell4Casts,
                                    @Summoner1Casts, @Summoner1Id, @Summoner2Casts,
                                    @Summoner2Id, @SummonerId, @SummonerLevel,
                                    @SummonerName, @TeamEarlySurrendered, @TeamID,
                                    @TimeCCingOthers, @TimePlayed, @TotalDamageDealt,
                                    @TotalDamageDealtToChampions, @TotalDamageShieldedOnTeammates, @TotalDamageTaken,
                                    @TotalHeal, @TotalHealsOnTeammates, @TotalMinionsKilled,
                                    @TotalTimeCCDealt, @TotalTimeSpentDead, @TotalUnitsHealed,
                                    @TowerKills, @TowerTakedowns, @TowersLost, @TripleKills,
                                    @TrueDamageDealt, @TrueDamageDealtToChampions, @TrueDamageTaken,
                                    @UnrealKills, @VisionScore, @VisionWardsBoughtInGame,
                                    @WardsKilled, @WardsPlaced)";
                        foreach (TeamDB teamDB in matchDB.Teams)
                        {
                            foreach (ParticipantDB participantDB in teamDB.Participants)
                                if (await connection.ExecuteAsync(sql, participantDB, transaction) < 1)
                                {
                                    transaction.Rollback();
                                    return false;
                                }
                        }

                        transaction.Commit();

                        return true;
                    }
                }
                catch (Exception ex)
                {
                    m_logger.LogError(ex, ex.Message);
                    transaction?.Rollback();
                    return false;
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }

        public async Task<MatchDB> GetMatchAsync(string matchID)
        {
            try
            {
                string sql = @"SELECT * FROM [dbo].[Match] WHERE [MatchID]=@MatchID";
                using (SqlConnection connection = new SqlConnection(m_connectionString))
                {
                    connection.Open();
                    MatchDB matchDB = await connection.QueryFirstOrDefaultAsync<MatchDB>(sql, new { MatchID = matchID });
                    if (matchDB == null)
                        return null;
                    sql = @"SELECT * FROM [dbo].[Team] WHERE [MatchID]=@MatchID";
                    matchDB.Teams = (await connection.QueryAsync<TeamDB>(sql, new { MatchID = matchID })).ToList();
                    sql = @"SELECT * FROM [dbo].[Participant] WHERE [TeamID]=@TeamID";
                    foreach (var team in matchDB.Teams)
                    {
                        team.Participants = (await connection.QueryAsync<ParticipantDB>(sql, new { TeamID = team.TeamId })).ToList();
                    }
                    return matchDB;
                }
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex, ex.Message);
            }

            return null;
        }

        public async Task<List<MatchDB>> GetMatchesAsync(int limit, string summonername, long? from, long? to)
        {
            //Filter times
            if (from == null)
                from = 0;
            if (to == null)
                to = long.MaxValue;
            try
            {
                string sql = null;
                if (summonername == null)
                    sql = @"SELECT TOP (@Limit) MatchID FROM [dbo].[Match]
                            WHERE [GameStartTimestamp]>=@from AND [GameStartTimestamp]<@to ORDER BY [GameStartTimestamp] DESC";
                else
                    sql = @"SELECT m.[MatchID] FROM
                                (SELECT DISTINCT  TOP(@Limit) m.[MatchID], m.[GameStartTimestamp] FROM [dbo].[Match] m
                                    INNER JOIN
                                        (SELECT [MatchID] FROM [dbo].Team t
                                            INNER JOIN
                                                (SELECT [TeamID] FROM [dbo].[Participant] WHERE [SummonerName]=@SummonerName) p ON p.TeamID = t.TeamID
                                        ) t ON t.MatchID = m.MatchID
                                    WHERE m.[GameStartTimestamp]>=@from AND m.[GameStartTimestamp]<@to ORDER BY m.[GameStartTimestamp] DESC
                                    ) m;";
                using (SqlConnection connection = new SqlConnection(m_connectionString))
                {
                    connection.Open();
                    List<string> matchIDs = (await connection.QueryAsync<string>(sql, new { Limit = limit, From = from, to = to, SummonerName = summonername })).ToList();
                    List<MatchDB> resultMatches = new List<MatchDB>();
                    foreach (var matchID in matchIDs)
                    {
                        var completeMatch = await GetMatchAsync(matchID);
                        resultMatches.Add(completeMatch);
                    }
                    return resultMatches;
                }
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex, ex.Message);
            }

            return null;
        }

        private string m_connectionString;
        private ILogger<SummonerMSSQLRepo> m_logger;
        private DBSettings m_settings;
    }
}