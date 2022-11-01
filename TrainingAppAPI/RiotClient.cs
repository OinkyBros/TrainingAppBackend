using Microsoft.Extensions.Logging;
using Oinky.TrainingAppAPI.Models.Configuration;
using Oinky.TrainingAppAPI.Models.RiotAPI;

namespace Oinky.TrainingAppAPI
{
    public class RiotClient
    {
        public static RiotClient Instance => m_instance;

        private RiotClient(RiotClientSettings settings, ILogger<RiotClient> logger)
        {
            m_settings = settings;
            m_logger = logger;
        }

        public static RiotClient Init(RiotClientSettings settings, ILogger<RiotClient> logger)
        {
            if (m_instance == null)
            {
                lock (m_lock)
                {
                    if (m_instance == null)
                    {
                        var instance = new RiotClient(settings, logger);
                        if (instance.Init())
                            m_instance = instance;
                    }
                }
            }
            return m_instance;
        }

        private bool Init()
        {
            try
            {
                m_httpClient = new HttpClient()
                {
                    BaseAddress = new Uri("https://europe.api.riotgames.com")
                };
                m_httpClient.DefaultRequestHeaders.Add("X-Riot-Token", m_settings.ApiKey);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<MatchRiotDTO> FetchMatchAsync(string matchID)
        {
            HttpResponseMessage result;
            try
            {
                var url = "/lol/match/v5/matches/" + matchID;
                result = await m_httpClient.GetAsync(url);
                if (result.IsSuccessStatusCode)
                    return await result.Content.ReadFromJsonAsync<MatchRiotDTO>();
                else if (result.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    m_logger.LogWarning("To many requests. Wait for 60 Seconds");
                    await Task.Delay(60 * 1000);
                    return await FetchMatchAsync(matchID);
                }
                m_logger.LogWarning("Unknown Statuscode in FetchMatchAsync: " + result.StatusCode);
            }
            catch (Exception ex)
            {
                m_logger.LogError(exception: ex, message: ex.Message);
            }

            return null;
        }

        public async Task<List<string>> FetchMatchIDsAsync(string puuid, long startTimestamp, int limit = 20)
        {
            try
            {
                var url = String.Format("/lol/match/v5/matches/by-puuid/{0}/ids?startTime={1}&count={2}", puuid, startTimestamp, limit);
                var result = await m_httpClient.GetAsync(url);
                if (result.IsSuccessStatusCode)
                    return await result.Content.ReadFromJsonAsync<List<string>>();
                else if (result.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    m_logger.LogWarning("To many requests. Wait for 60 Seconds");
                    await Task.Delay(60 * 1000);
                    return await FetchMatchIDsAsync(puuid, startTimestamp, limit);
                }
                m_logger.LogWarning("Unknown Statuscode in FetchMatchAsync: " + result.StatusCode);
            }
            catch (Exception ex)
            {
                m_logger.LogError(exception: ex, message: ex.Message);
            }
            return null;
        }

        public async Task<SummonerRiotDTO> FetchSummonerAsync(string summonerName)
        {
            try
            {
                var url = "https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-name/" + summonerName;
                var result = await m_httpClient.GetAsync(url);
                if (result.IsSuccessStatusCode)
                    return await result.Content.ReadFromJsonAsync<SummonerRiotDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        private static RiotClient m_instance = null;
        private static object m_lock = new();
        private RiotClientSettings m_settings;
        private ILogger<RiotClient> m_logger;
        private HttpClient m_httpClient;
    }
}