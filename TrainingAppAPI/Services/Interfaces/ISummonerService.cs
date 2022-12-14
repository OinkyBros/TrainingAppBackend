using Oinky.TrainingAppAPI.Models.DB;

namespace Oinky.TrainingAppAPI.Services.Interfaces
{
    public interface ISummonerService
    {
        public Task<bool> AddSummonerAsync(SummonerDB summoner);

        public Task<SummonerDB> GetSummonerAsync(string puuid);

        public Task<List<SummonerDB>> GetSummonersAsync();

        public Task<bool> UpdateUserAsync(SummonerDB summoner);
    }
}