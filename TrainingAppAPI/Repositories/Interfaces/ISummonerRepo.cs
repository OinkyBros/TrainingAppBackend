using Oinky.TrainingAppAPI.Models.DB;

namespace Oinky.TrainingAppAPI.Repositories.Interfaces
{
    public interface ISummonerRepo
    {
        Task<bool> AddSummonerAsync(SummonerDB summoner);
        Task<List<SummonerDB>> GetSummonersAsync();
        Task<SummonerDB> GetSummonerAsync(string puuid);
    }
}
