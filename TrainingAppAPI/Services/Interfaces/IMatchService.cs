using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Models.Result;

namespace Oinky.TrainingAppAPI.Services.Interfaces
{
    public interface IMatchService
    {
        Task<List<MatchDTO>> GetMatchesAsync(int limit, string summonername, long? from, long? to);

        Task<ExtendedMatchDTO> GetMatchAsync(string matchID);

        Task<bool> AddMatchAsync(MatchDB matchDB);
    }
}