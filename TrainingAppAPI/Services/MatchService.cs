﻿using Oinky.TrainingAppAPI.Models.DB;
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

        public async Task<List<MatchDTO>> GetMatchesAsync(int limit, string summonername = null, long? from = null, long? to = null)
        {
            return await m_matchRepo.GetMatchesAsync(limit, summonername, from, to);
        }

        public async Task<bool> AddMatchAsync(MatchDB matchDB)
        {
            return await m_matchRepo.AddMatchAsync(matchDB);
        }

        public async Task<ExtendedMatchDTO> GetMatchAsync(string matchID)
        {
            return await m_matchRepo.GetMatchAsync(matchID);
        }

        private IMatchRepo m_matchRepo;
    }
}