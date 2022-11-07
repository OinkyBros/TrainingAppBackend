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

        public async Task<SummonerDB> GetSummonerAsync(string puuid)
        {
            if (!m_summoners.ContainsKey(puuid))
                return null;
            SummonerDB summoner = null;
            while (summoner == null)
                if (!m_summoners.TryGetValue(puuid, out summoner))
                    await Task.Delay(100);
            return summoner;
        }

        public Task<List<SummonerDB>> GetSummonersAsync()
        {
            return Task.FromResult(m_summoners.Values.ToList());
        }

        public async Task<bool> UpdateUserAsync(SummonerDB summoner)
        {
            if (!m_summoners.ContainsKey(summoner.PUUID))
                return false;
            SummonerDB oldValue = await GetSummonerAsync(summoner.PUUID);
            while (!m_summoners.TryUpdate(summoner.PUUID, summoner, oldValue))
                await Task.Delay(100);
            return true;
        }

        private static ConcurrentDictionary<string, SummonerDB> m_summoners = new ConcurrentDictionary<string, SummonerDB>();
    }
}