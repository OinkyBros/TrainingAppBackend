using Oinky.TrainingAppAPI.Models.DB;
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

        public async Task<List<Match>> GetMatchesAsync()
        {
            return await m_matchRepo.GetMatchesAsync();
        }

        public async Task<bool> AddMatchAsync(MatchDB matchDB)
        {
            return await m_matchRepo.AddMatchAsync(matchDB);
        }

        private IMatchRepo m_matchRepo;
    }
}