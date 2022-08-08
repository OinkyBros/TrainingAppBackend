using Oinky.TrainingAppAPI.Models.Configuration;
using Oinky.TrainingAppAPI.Models.RiotAPI;

namespace Oinky.TrainingAppAPI
{
    public class RiotClient
    {
        public static RiotClient Instance => m_instance;

        private RiotClient(RiotClientSettings settings)
        {
            m_settings = settings;
        }

        public static RiotClient Init(RiotClientSettings settings)
        {
            if (m_instance == null)
            {
                lock (m_lock)
                {
                    if (m_instance == null)
                    {
                        var instance = new RiotClient(settings);
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
            try
            {
                var url = "/lol/match/v5/matches/" + matchID;
                var result = await m_httpClient.GetAsync(url);
                if (result.IsSuccessStatusCode)
                {
                    var match = await result.Content.ReadFromJsonAsync<MatchRiotDTO>();
                    return match;
                }
            } catch(Exception)
            {

            }

            return null;
        }

        private static RiotClient m_instance = null;
        private static object m_lock = new();
        private RiotClientSettings m_settings;
        private HttpClient m_httpClient;
    }
}