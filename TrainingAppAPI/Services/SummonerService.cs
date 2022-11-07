using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Repositories.Interfaces;
using Oinky.TrainingAppAPI.Services.Interfaces;

namespace Oinky.TrainingAppAPI.Services
{
    public class SummonerService : ISummonerService
    {
        public SummonerService(ISummonerRepo summonerRepo)
        {
            m_summonerRepo = summonerRepo;
        }

        public Task<bool> AddSummonerAsync(SummonerDB summoner)
        {
            return m_summonerRepo.AddSummonerAsync(summoner);
        }

        public Task<SummonerDB> GetSummonerAsync(string puuid)
        {
            return m_summonerRepo.GetSummonerAsync(puuid);
        }

        public Task<List<SummonerDB>> GetSummonersAsync()
        {
            return m_summonerRepo.GetSummonersAsync();
        }

        public Task<bool> UpdateUserAsync(SummonerDB summoner)
        {
            return m_summonerRepo.UpdateUserAsync(summoner);
        }

        private ISummonerRepo m_summonerRepo;
    }
}