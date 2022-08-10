using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Models.Result;

namespace Oinky.TrainingAppAPI.Repositories.Interfaces
{
    public interface IMatchRepo
    {
        Task<bool> AddMatchAsync(MatchDB matchDB);

        Task<bool> AddMatchesAsync(List<MatchDB> matches);

        Task<ExtendedMatchDTO> GetMatchAsync(string matchID);

        Task<List<MatchDTO>> GetMatchesAsync(int limit, string summonername, long? from, long? to);
    }
}