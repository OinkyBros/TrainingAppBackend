using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Models.Extensions;
using Oinky.TrainingAppAPI.Models.RiotAPI;
using Oinky.TrainingAppAPI.Services.Interfaces;
using Oinky.TrainingAppAPI.Utils;

namespace Oinky.TrainingAppAPI
{
    public class DataFetcher
    {
        public DataFetcher(IConfiguration configuration, ISummonerService summonerService, IMatchService matchService)
        {
            m_config = configuration;
            m_summonerService = summonerService;
            m_matchService = matchService;
        }

        public async Task RunAsync(CancellationToken token)
        {
            //Get Riot Client
            m_riotClient = RiotClient.Instance;
            //Get Registered Summoners
            List<SummonerDB> summoners = await m_summonerService.GetSummonersAsync();
            if (summoners == null || summoners.Count == 0)
                await GetInitialSummonersAsync();
            summoners = await m_summonerService.GetSummonersAsync();
            long timestamp = FIRST_TIMESTAMP;
            int limit = 100;
            //Catchup for every summoner
            foreach (SummonerDB summoner in summoners)
            {
                timestamp = FIRST_TIMESTAMP;
                bool fetchMore = true;
                while (fetchMore)
                {
                    List<string> matchIDs = await m_riotClient.FetchMatchIDsAsync(summoner.PUUID, timestamp, limit);
                    foreach (string matchID in matchIDs)
                    {
                        MatchRiotDTO matchDTO = await m_riotClient.FetchMatchAsync(matchID);
                        //Check GameMode
                        if (MatchExtension.ConvertRiotMode(matchDTO.Info.QueueId) < 0)
                            continue;
                        //Check if enough Oinkies
                        if (matchDTO.Info.Participants.Select(p => MatchExtension.CheckIfOinky(p.SummonerName)).ToList().Count < 3)
                            continue;
                        timestamp = Math.Max(timestamp, matchDTO.Info.GameStartTimestamp / 1000);
                        await m_matchService.AddMatchAsync(matchDTO.ToDBModel());
                    }
                    fetchMore = matchIDs.Count > limit;
                }
            }
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(60 * 1000);
                timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                summoners = await m_summonerService.GetSummonersAsync();
                foreach (SummonerDB summoner in summoners)
                {
                    List<string> matchIDs = await m_riotClient.FetchMatchIDsAsync(summoner.PUUID, timestamp);
                    foreach (string matchID in matchIDs)
                    {
                        MatchRiotDTO matchDTO = await m_riotClient.FetchMatchAsync(matchID);
                        //Check GameMode
                        if (MatchExtension.ConvertRiotMode(matchDTO.Info.QueueId) < 0)
                            continue;
                        //Check if enough Oinkies
                        if (matchDTO.Info.Participants.Select(p => MatchExtension.CheckIfOinky(p.SummonerName)).ToList().Count < 3)
                            continue;
                        timestamp = Math.Max(timestamp, matchDTO.Info.GameStartTimestamp / 1000);
                        await m_matchService.AddMatchAsync(matchDTO.ToDBModel());
                    }
                }
            }
        }

        private async Task<List<SummonerDB>> GetInitialSummonersAsync()
        {
            List<SummonerDB> result = new List<SummonerDB>();
            foreach (var summoner in APIUtils.OINKIES)
            {
                SummonerRiotDTO dto = await m_riotClient.FetchSummonerAsync(summoner);
                if (dto == null)
                    continue;
                SummonerDB dbModel = dto.ToDBModel();
                result.Add(dbModel);
                await m_summonerService.AddSummonerAsync(dbModel);
            }
            return result;
        }

        private static readonly long FIRST_TIMESTAMP = 1659304800;
        private IConfiguration m_config;
        private IMatchService m_matchService;
        private RiotClient m_riotClient;
        private ISummonerService m_summonerService;
    }
}