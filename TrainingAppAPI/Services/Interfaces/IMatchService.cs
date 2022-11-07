using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Models.Result;

namespace Oinky.TrainingAppAPI.Services.Interfaces
{
    public interface IMatchService
    {
        Task<bool> AddMatchAsync(MatchDB matchDB);

        Task<ExtendedMatchDTO> GetExtendedMatchAsync(string matchID);

        Task<List<ExtendedMatchDTO>> GetExtendedMatchesAsync(int limit, string summonername, long? from, long? to);

        Task<MatchDTO> GetMatchAsync(string matchID);

        Task<List<MatchDTO>> GetMatchesAsync(int limit, string summonername, long? from, long? to);
    }
}