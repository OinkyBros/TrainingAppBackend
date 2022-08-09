﻿using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Models.Extensions;
using Oinky.TrainingAppAPI.Models.Result;
using Oinky.TrainingAppAPI.Repositories.Interfaces;
using System.Collections.Concurrent;

namespace Oinky.TrainingAppAPI.Repositories
{
    public class MatchFakeRepo : IMatchRepo
    {
        public async Task<bool> AddMatchAsync(MatchDB matchDB)
        {
            if (m_matches.ContainsKey(matchDB.MatchID))
                return false;
            if (!m_matches.TryAdd(matchDB.MatchID, matchDB))
                await Task.Delay(100);
            return true;
        }

        public Task<bool> AddMatchesAsync(List<MatchDB> matches)
        {
            throw new NotImplementedException();
        }

        public async Task<ExtendedMatchResultDTO> GetMatchAsync(string matchID)
        {
            if (!m_matches.ContainsKey(matchID))
                return null;
            if (!m_matches.TryGetValue(matchID, out MatchDB match))
                await Task.Delay(100);
            return match.ToExtendedResultModel();
        }

        public Task<List<MatchResultDTO>> GetMatchesAsync(int limit, string summonername, long? from, long? to)
        {
            List<MatchDB> results;
            //Filter for summonername
            if (summonername != null)
                results = m_matches.Values
                    .Where(m => m.Teams.Where(team => team.Participants.Any(p => p.SummonerName == summonername)).ToList().Count > 0)
                    .OrderByDescending(m => m.GameStartTimestamp).ToList();
            else
                results = m_matches.Values.OrderByDescending(m => m.GameStartTimestamp).ToList();

            //Filter times
            if (from == null)
                from = 0;
            if (to == null)
                to = long.MaxValue;
            results = results.Where(m => m.GameStartTimestamp >= from && m.GameStartTimestamp <= to).ToList();

            //Limit
            results = results.Take(limit).ToList();

            //Convert
            List<MatchResultDTO> resultDTOs = new List<MatchResultDTO>();
            foreach (MatchDB matchDB in results)
                resultDTOs.Add(matchDB.ToResultModel());

            return Task.FromResult(resultDTOs);
        }

        private static ConcurrentDictionary<string, MatchDB> m_matches = new ConcurrentDictionary<string, MatchDB>();
    }
}