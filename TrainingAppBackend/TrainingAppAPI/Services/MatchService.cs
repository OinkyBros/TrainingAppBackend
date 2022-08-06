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

        private IMatchRepo m_matchRepo;
    }
}