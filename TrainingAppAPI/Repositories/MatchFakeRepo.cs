using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Models.Result;
using Oinky.TrainingAppAPI.Repositories.Interfaces;
using System.Collections.Concurrent;

namespace Oinky.TrainingAppAPI.Repositories
{
    public class MatchFakeRepo : IMatchRepo
    {
        public Task<List<Match>> GetMatchesAsync()
        {
            return Task.FromResult(new List<Match>());
        }

        public Task<bool> AddMatchesAsync(List<MatchDB> matches)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddMatchAsync(MatchDB matchDB)
        {
            if (m_matches.ContainsKey(matchDB.MatchID))
                return false;
            if (!m_matches.TryAdd(matchDB.MatchID, matchDB))
                await Task.Delay(100);
            return true;
        }

        private static ConcurrentDictionary<string, MatchDB> m_matches = new ConcurrentDictionary<string, MatchDB>();
    }
}