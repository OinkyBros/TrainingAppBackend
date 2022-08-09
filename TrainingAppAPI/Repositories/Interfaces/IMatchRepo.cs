using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Models.Result;

namespace Oinky.TrainingAppAPI.Repositories.Interfaces
{
    public interface IMatchRepo
    {
        Task<List<MatchResultDTO>> GetMatchesAsync(int limit, string summonername, long? from, long? to);

        Task<bool> AddMatchesAsync(List<MatchDB> matches);

        Task<bool> AddMatchAsync(MatchDB matchDB);
    }
}