using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Models.Extensions;
using Oinky.TrainingAppAPI.Models.Result;
using Oinky.TrainingAppAPI.Repositories.Interfaces;
using Oinky.TrainingAppAPI.Services.Interfaces;

namespace Oinky.TrainingAppAPI.Services
{
    public class MatchService : IMatchService
    {
        public MatchService(IMatchRepo matchRepo)
        {
            m_matchRepo = matchRepo;
        }

        public async Task<bool> AddMatchAsync(MatchDB matchDB)
        {
            return await m_matchRepo.AddMatchAsync(matchDB);
        }

        public async Task<ExtendedMatchDTO> GetExtendedMatchAsync(string matchID)
        {
            MatchDB matchDB = await m_matchRepo.GetMatchAsync(matchID);
            return matchDB?.ToExtendedResultModel();
        }

        public async Task<List<ExtendedMatchDTO>> GetExtendedMatchesAsync(int limit, string summonername, long? from, long? to)
        {
            List<ExtendedMatchDTO> matches = new List<ExtendedMatchDTO>();
            List<MatchDB> dbMatches = await m_matchRepo.GetMatchesAsync(limit, summonername, from, to);
            if (dbMatches != null)
                foreach (MatchDB matchDB in dbMatches)
                    matches.Add(matchDB.ToExtendedResultModel());
            return matches;
        }

        public async Task<MatchDTO> GetMatchAsync(string matchID)
        {
            MatchDB matchDB = await m_matchRepo.GetMatchAsync(matchID);
            return matchDB?.ToResultModel();
        }

        public async Task<List<MatchDTO>> GetMatchesAsync(int limit, string summonername = null, long? from = null, long? to = null)
        {
            List<MatchDTO> matches = new List<MatchDTO>();
            List<MatchDB> dbMatches = await m_matchRepo.GetMatchesAsync(limit, summonername, from, to);
            if (dbMatches != null)
                foreach (MatchDB matchDB in dbMatches)
                    matches.Add(matchDB.ToResultModel());
            return matches;
        }

        private IMatchRepo m_matchRepo;
    }
}