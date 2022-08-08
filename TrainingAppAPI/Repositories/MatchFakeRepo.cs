using Oinky.TrainingAppAPI.Models.Extensions;
using Oinky.TrainingAppAPI.Models.Result;
using Oinky.TrainingAppAPI.Models.RiotAPI;
using Oinky.TrainingAppAPI.Repositories.Interfaces;

namespace Oinky.TrainingAppAPI.Repositories
{
    public class MatchFakeRepo : IMatchRepo
    {
        public static void InitFake()
        {
            var client = RiotClient.Instance;
            m_matches = new List<Match>();
            if (client == null)
                return;
            foreach (string matchID in MATCH_IDS)
            {
                var match = client.FetchMatchAsync(matchID).Result;
                if (match != null)
                    m_matches.Add(match.ToResultModel());
            }
        }

        public Task<List<Match>> GetMatchesAsync()
        {
            return Task.FromResult(m_matches);
        }

        private static readonly List<string> MATCH_IDS = new List<string>
        {
            "EUW1_5996514311",
            "EUW1_5996416741",
            "EUW1_5996300107",
            "EUW1_5984423059",
            "EUW1_5984323813",
            "EUW1_5984204936",
            "EUW1_5983712067",
            "EUW1_5983528947",
            "EUW1_5983476840",
            "EUW1_5982976304",
            "EUW1_5982927025",
            "EUW1_5982852228",
            "EUW1_5982756082",
            "EUW1_5982661362",
            "EUW1_5982587112",
            "EUW1_5982527453",
            "EUW1_5982464762",
            "EUW1_5979908928",
            "EUW1_5979913862",
            "EUW1_5979829237"
        };

        private static List<Match> m_matches = null;
    }
}