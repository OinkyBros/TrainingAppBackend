using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Repositories.Interfaces;
using System.Collections.Concurrent;

namespace Oinky.TrainingAppAPI.Repositories
{
    public class SummonerFakeRepo : ISummonerRepo
    {
        public async Task<bool> AddSummonerAsync(SummonerDB summoner)
        {
            if (!m_summoners.TryAdd(summoner.PUUID, summoner))
                await Task.Delay(100);
            return true;
        }

        public Task<SummonerDB> GetSummonerAsync(string puuid)
        {
            throw new NotImplementedException();
        }

        public Task<List<SummonerDB>> GetSummonersAsync()
        {
            return Task.FromResult(m_summoners.Values.ToList());
        }

        private static ConcurrentDictionary<string, SummonerDB> m_summoners = new ConcurrentDictionary<string, SummonerDB>();
    }
}