using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Models.Extensions;
using Oinky.TrainingAppAPI.Models.Result;
using Oinky.TrainingAppAPI.Models.RiotAPI;
using Oinky.TrainingAppAPI.Services.Interfaces;
using Oinky.TrainingAppAPI.Utils;

namespace Oinky.TrainingAppAPI
{
    public class DataFetcher
    {
        public DataFetcher(ILogger<DataFetcher> logger, IConfiguration configuration, ISummonerService summonerService, IMatchService matchService)
        {
            m_config = configuration;
            m_summonerService = summonerService;
            m_matchService = matchService;
            m_logger = logger;
        }

        public async Task RunAsync(CancellationToken token)
        {
            //Get Riot Client
            m_riotClient = RiotClient.Instance;
            //Get Registered Summoners
            List<SummonerDB> summoners = await GetInitialSummonersAsync();

            int limit = 100;
            //Catchup for every summoner
            m_logger.LogInformation("Catchup games");
            foreach (SummonerDB summoner in summoners)
            {
                m_logger.LogInformation("Get matches for: " + summoner.DisplayName);
                bool fetchMore = true;
                while (fetchMore)
                {
                    List<string> matchIDs = await m_riotClient.FetchMatchIDsAsync(summoner.PUUID, summoner.LastUpdate, limit);
                    foreach (string matchID in matchIDs)
                    {
                        //Check if match is present
                        MatchDTO matchDTO = await m_matchService.GetMatchAsync(matchID);
                        if (matchDTO == null)
                        {
                            MatchRiotDTO riotMatch = null;
                            do
                            {
                                riotMatch = await m_riotClient.FetchMatchAsync(matchID);
                            } while (riotMatch == null);
                            summoner.LastUpdate = Math.Max(summoner.LastUpdate, riotMatch.Info.GameStartTimestamp / 1000);
                            //Check Game Mode
                            if (MatchExtension.ConvertRiotMode(riotMatch.Info.QueueId) < 0)
                                continue;
                            //Check if enough Oinkies
                            if (riotMatch.Info.Participants.Where(p => MatchExtension.CheckIfOinky(p.SummonerName)).ToList().Count < 3)
                                continue;
                            //Add match to DB
                            while (!await m_matchService.AddMatchAsync(riotMatch.ToDBModel()))
                            {
                                await Task.Delay(100);
                            };
                        }
                        else
                            summoner.LastUpdate = Math.Max(summoner.LastUpdate, matchDTO.GameStart);
                    }
                    await m_summonerService.UpdateUserAsync(summoner);
                    fetchMore = limit == matchIDs.Count;
                }
            }

            m_logger.LogInformation("Finished Initialisation");
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(60 * 1000);
                summoners = await m_summonerService.GetSummonersAsync();
                foreach (SummonerDB summoner in summoners)
                {
                    m_logger.LogInformation("Update: " + summoner.DisplayName);
                    List<string> matchIDs = await m_riotClient.FetchMatchIDsAsync(summoner.PUUID, summoner.LastUpdate);
                    foreach (string matchID in matchIDs)
                    {
                        //Check if match is present
                        MatchDTO matchDTO = await m_matchService.GetMatchAsync(matchID);
                        if (matchDTO == null)
                        {
                            MatchRiotDTO riotMatch = null;
                            do
                            {
                                riotMatch = await m_riotClient.FetchMatchAsync(matchID);
                            } while (riotMatch == null);
                            summoner.LastUpdate = Math.Max(summoner.LastUpdate, riotMatch.Info.GameStartTimestamp / 1000);
                            //Check Game Mode
                            if (MatchExtension.ConvertRiotMode(riotMatch.Info.QueueId) < 0)
                                continue;
                            //Check if enough Oinkies
                            if (riotMatch.Info.Participants.Where(p => MatchExtension.CheckIfOinky(p.SummonerName)).ToList().Count < 3)
                                continue;
                            //Add match to DB
                            while (!await m_matchService.AddMatchAsync(riotMatch.ToDBModel()))
                            {
                                await Task.Delay(100);
                            };
                        }
                        else
                            summoner.LastUpdate = Math.Max(summoner.LastUpdate, matchDTO.GameStart);
                    }
                    await m_summonerService.UpdateUserAsync(summoner);
                }
            }
        }

        private async Task<List<SummonerDB>> GetInitialSummonersAsync()
        {
            m_logger.LogInformation("Get Initial Summoners");
            List<SummonerDB> summoners = await m_summonerService.GetSummonersAsync();
            if (summoners == null)
                summoners = new List<SummonerDB>();

            //Get initial catalog
            List<string> oinkyCatalog = APIUtils.OINKIES;
            SummonerRiotDTO oinkyRiot = null;
            foreach (string oinky in oinkyCatalog)
            {
                if (summoners.Where(s => s.DisplayName == oinky).ToList().Count() != 0)
                    continue;
                do
                {
                    oinkyRiot = await m_riotClient.FetchSummonerAsync(oinky);
                    if (oinkyRiot == null)
                        continue;
                    SummonerDB oinkyDB = oinkyRiot.ToDBModel();
                    oinkyDB.LastUpdate = FIRST_TIMESTAMP;
                    if (!await m_summonerService.AddSummonerAsync(oinkyDB))
                        oinkyRiot = null;
                    else
                        summoners.Add(oinkyDB);
                } while (oinkyRiot == null);
                oinkyRiot = null;
            }
            return summoners;
        }

        private static readonly long FIRST_TIMESTAMP = 1659304800;
        private IConfiguration m_config;
        private ILogger<DataFetcher> m_logger;
        private IMatchService m_matchService;
        private RiotClient m_riotClient;
        private ISummonerService m_summonerService;
    }
}