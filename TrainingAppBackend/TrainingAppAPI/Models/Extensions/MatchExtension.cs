using Oinky.TrainingAppAPI.Models.Result;
using Oinky.TrainingAppAPI.Models.RiotAPI;

namespace Oinky.TrainingAppAPI.Models.Extensions
{
    public static class MatchExtension
    {
        public static Match ToResultModel(this MatchRiotDTO dto)
        {
            Match resultMatch = new Match();

            //Mode
            int mode = ConvertRiotMode(dto.Info.QueueId);
            if (mode < 0)
                return null;
            resultMatch.Mode = (GameMode)mode;
            //Get Duration
            resultMatch.Duration = (int)((dto.Info.GameEndTimestamp - dto.Info.GameStartTimestamp) / 1000);
            //Start Time
            resultMatch.GameStart = dto.Info.GameStartTimestamp;
            //ID
            resultMatch.MatchID = dto.Metadata.MatchId;
            //Teams
            resultMatch.Teams = new List<Team>();
            foreach (TeamRiotDTO team in dto.Info.Teams)
            {
                Team resultTeam = new Team()
                {
                    TeamID = team.TeamId,
                    Win = team.Win,
                    Participants = new List<Participant>()
                };
                List<ParticipantRiotDTO> participants = dto.Info.Participants.Where(p => p.TeamId == team.TeamId).ToList();
                foreach (ParticipantRiotDTO p in participants)
                {
                    Participant participant = new Participant()
                    {
                        Champion = p.ChampionName,
                        SummonerID = p.SummonerId,
                        SummonerName = p.SummonerName,
                        IsOinky = CheckIfOinky(p.SummonerName),
                        Role = ConvertRole(p.TeamPosition)
                    };
                    resultTeam.Participants.Add(participant);
                }
                resultMatch.Teams.Add(resultTeam);
            }
            return resultMatch;
        }

        private static bool CheckIfOinky(string summonerName)
        {
            return OINKIES.Contains(summonerName);
        }

        private static int ConvertRiotMode(int mode)
        {
            //See: https://static.developer.riotgames.com/docs/lol/queues.json
            switch (mode)
            {
                case 400:
                    return (int)GameMode.NORMAL;

                case 420:
                    return (int)GameMode.DUO;

                case 440:
                    return (int)GameMode.FLEX;

                case 700:
                    return (int)GameMode.CLASH;

                default:
                    return -1;
            }
        }

        private static Role ConvertRole(string teamPosition)
        {
            switch (teamPosition)
            {
                case "TOP":
                    return Role.TOP;

                case "JUNGLE":
                    return Role.JUNGLE;

                case "MIDDLE":
                    return Role.MID;

                case "BOTTOM":
                    return Role.BOT;

                case "UTILITY":
                    return Role.SUPP;

                default:
                    return Role.UNDEFINED;
            }
        }

        private static readonly List<string> OINKIES = new List<string>()
        {
            "Ploinky",
            "Toinky",
            "Stroinky",
            "Voinky",
            "Daray"
        };
    }
}