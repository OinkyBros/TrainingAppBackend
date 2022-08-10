using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Models.Enums;
using Oinky.TrainingAppAPI.Models.Result;
using Oinky.TrainingAppAPI.Models.RiotAPI;
using Oinky.TrainingAppAPI.Utils;

namespace Oinky.TrainingAppAPI.Models.Extensions
{
    public static class MatchExtension
    {
        public static MatchDB ToDBModel(this MatchRiotDTO dto)
        {
            List<TeamDB> teams = new List<TeamDB>();
            foreach (TeamRiotDTO teamDTO in dto.Info.Teams)
            {
                TeamDB teamDB = new TeamDB()
                {
                    Bans = String.Join(',', teamDTO.Bans.OrderBy(t => t.PickTurn).Select(t => t.ChampionId.ToString()).ToArray()),
                    Barons = teamDTO.Objectives.Baron.Kills,
                    ChampionKills = teamDTO.Objectives.Champion.Kills,
                    Dragons = teamDTO.Objectives.Dragon.Kills,
                    FirstBaron = teamDTO.Objectives.Baron.First,
                    FirstBlood = teamDTO.Objectives.Champion.First,
                    FirstDragon = teamDTO.Objectives.Dragon.First,
                    FirstHerald = teamDTO.Objectives.RiftHerald.First,
                    FirstInhibitors = teamDTO.Objectives.Inhibitor.First,
                    FirstTower = teamDTO.Objectives.Tower.First,
                    Heralds = teamDTO.Objectives.RiftHerald.Kills,
                    IngameID = teamDTO.TeamId,
                    Inhibitors = teamDTO.Objectives.Inhibitor.Kills,
                    TeamID = Guid.NewGuid(),
                    Towers = teamDTO.Objectives.Tower.Kills,
                    Win = teamDTO.Win,
                    Participants = new List<ParticipantDB>()
                };
                foreach (ParticipantRiotDTO participantDTO in dto.Info.Participants.Where(p => p.TeamId == teamDTO.TeamId))
                {
                    ParticipantDB participantDB = new ParticipantDB()
                    {
                        Assists = participantDTO.Assists,
                        BaronKills = participantDTO.BaronKills,
                        BasicPings = participantDTO.BasicPings,
                        BountyLevel = participantDTO.BountyLevel,
                        ChampExperience = participantDTO.ChampExperience,
                        ChampionId = participantDTO.ChampionId,
                        ChampionName = participantDTO.ChampionName,
                        ChampionTransform = participantDTO.ChampionTransform,
                        ChampLevel = participantDTO.ChampLevel,
                        ConsumablesPurchased = participantDTO.ConsumablesPurchased,
                        DamageDealtToBuildings = participantDTO.DamageDealtToBuildings,
                        DamageDealtToObjectives = participantDTO.DamageDealtToObjectives,
                        DamageDealtToTurrets = participantDTO.DamageDealtToTurrets,
                        DamageSelfMitigated = participantDTO.DamageSelfMitigated,
                        Deaths = participantDTO.Deaths,
                        DetectorWardsPlaced = participantDTO.DetectorWardsPlaced,
                        DoubleKills = participantDTO.DoubleKills,
                        DragonKills = participantDTO.DragonKills,
                        EligibleForProgression = participantDTO.EligibleForProgression,
                        FirstBloodAssist = participantDTO.FirstBloodAssist,
                        FirstBloodKill = participantDTO.FirstBloodKill,
                        FirstTowerAssist = participantDTO.FirstTowerAssist,
                        FirstTowerKill = participantDTO.FirstTowerKill,
                        GameEndedInEarlySurrender = participantDTO.GameEndedInEarlySurrender,
                        GameEndedInSurrender = participantDTO.GameEndedInSurrender,
                        GoldEarned = participantDTO.GoldEarned,
                        GoldSpent = participantDTO.GoldSpent,
                        InhibitorKills = participantDTO.InhibitorKills,
                        InhibitorsLost = participantDTO.InhibitorsLost,
                        InhibitorTakedowns = participantDTO.InhibitorTakedowns,
                        Item0 = participantDTO.Item0,
                        Item1 = participantDTO.Item1,
                        Item2 = participantDTO.Item2,
                        Item3 = participantDTO.Item3,
                        Item4 = participantDTO.Item4,
                        Item5 = participantDTO.Item5,
                        Item6 = participantDTO.Item6,
                        ItemsPurchased = participantDTO.ItemsPurchased,
                        KillingSprees = participantDTO.KillingSprees,
                        Kills = participantDTO.Kills,
                        LargestCriticalStrike = participantDTO.LargestCriticalStrike,
                        LargestKillingSpree = participantDTO.LargestKillingSpree,
                        LargestMultiKill = participantDTO.LargestMultiKill,
                        LongestTimeSpentLiving = participantDTO.LongestTimeSpentLiving,
                        MagicDamageDealt = participantDTO.MagicDamageDealt,
                        MagicDamageDealtToChampions = participantDTO.MagicDamageDealtToChampions,
                        MagicDamageTaken = participantDTO.MagicDamageTaken,
                        NeutralMinionsKilled = participantDTO.NeutralMinionsKilled,
                        NexusKills = participantDTO.NexusKills,
                        NexusLost = participantDTO.NexusLost,
                        NexusTakedowns = participantDTO.NexusTakedowns,
                        ObjectivesStolen = participantDTO.ObjectivesStolen,
                        ObjectivesStolenAssists = participantDTO.ObjectivesStolenAssists,
                        ParticipantId = participantDTO.ParticipantId,
                        PentaKills = participantDTO.PentaKills,
                        PhysicalDamageDealt = participantDTO.PhysicalDamageDealt,
                        PhysicalDamageDealtToChampions = participantDTO.PhysicalDamageDealtToChampions,
                        PhysicalDamageTaken = participantDTO.PhysicalDamageTaken,
                        ProfileIcon = participantDTO.ProfileIcon,
                        Puuid = participantDTO.Puuid,
                        QuadraKills = participantDTO.QuadraKills,
                        RiotIdName = participantDTO.RiotIdName,
                        RiotIdTagline = participantDTO.RiotIdTagline,
                        Role = (Role)ConvertRole(participantDTO.TeamPosition),
                        SightWardsBoughtInGame = participantDTO.SightWardsBoughtInGame,
                        Spell1Casts = participantDTO.Spell1Casts,
                        Spell2Casts = participantDTO.Spell2Casts,
                        Spell3Casts = participantDTO.Spell3Casts,
                        Spell4Casts = participantDTO.Spell4Casts,
                        Summoner1Casts = participantDTO.Summoner1Casts,
                        Summoner1Id = participantDTO.Summoner1Id,
                        Summoner2Casts = participantDTO.Summoner2Casts,
                        Summoner2Id = participantDTO.Summoner2Id,
                        SummonerId = participantDTO.SummonerId,
                        SummonerLevel = participantDTO.SummonerLevel,
                        SummonerName = participantDTO.SummonerName,
                        TeamEarlySurrendered = participantDTO.TeamEarlySurrendered,
                        TimeCCingOthers = participantDTO.TimeCCingOthers,
                        TimePlayed = participantDTO.TimePlayed,
                        TotalDamageDealt = participantDTO.TotalDamageDealt,
                        TotalDamageDealtToChampions = participantDTO.TotalDamageDealtToChampions,
                        TotalDamageShieldedOnTeammates = participantDTO.TotalDamageShieldedOnTeammates,
                        TotalDamageTaken = participantDTO.TotalDamageTaken,
                        TotalHeal = participantDTO.TotalHeal,
                        TotalHealsOnTeammates = participantDTO.TotalHealsOnTeammates,
                        TotalMinionsKilled = participantDTO.TotalMinionsKilled,
                        TotalTimeCCDealt = participantDTO.TotalTimeCCDealt,
                        TotalTimeSpentDead = participantDTO.TotalTimeSpentDead,
                        TotalUnitsHealed = participantDTO.TotalUnitsHealed,
                        TowerKills = participantDTO.TurretKills,
                        TowersLost = participantDTO.TurretsLost,
                        TowerTakedowns = participantDTO.TurretTakedowns,
                        TripleKills = participantDTO.TripleKills,
                        TrueDamageDealt = participantDTO.TrueDamageDealt,
                        TrueDamageDealtToChampions = participantDTO.TrueDamageDealtToChampions,
                        TrueDamageTaken = participantDTO.TrueDamageTaken,
                        UnrealKills = participantDTO.UnrealKills,
                        VisionScore = participantDTO.VisionScore,
                        VisionWardsBoughtInGame = participantDTO.VisionWardsBoughtInGame,
                        WardsKilled = participantDTO.WardsKilled,
                        WardsPlaced = participantDTO.WardsPlaced
                    };
                    teamDB.Participants.Add(participantDB);
                }
                teams.Add(teamDB);
            }

            return new MatchDB()
            {
                MatchID = dto.Metadata.MatchId,
                GameCreation = (int)(dto.Info.GameCreation / 1000),
                GameDuration = (int)((dto.Info.GameEndTimestamp - dto.Info.GameStartTimestamp) / 1000),
                GameEndTimestamp = (int)(dto.Info.GameEndTimestamp / 1000),
                GameStartTimestamp = (int)(dto.Info.GameStartTimestamp / 1000),
                GameMode = (GameMode)ConvertRiotMode(dto.Info.QueueId),
                GameName = dto.Info.GameName,
                Teams = teams
            };
        }

        public static ExtendedMatchDTO ToExtendedResultModel(this MatchDB db)
        {
            ExtendedMatchDTO result = new ExtendedMatchDTO()
            {
                Duration = db.GameDuration,
                GameStart = db.GameStartTimestamp,
                MatchID = db.MatchID,
                Mode = db.GameMode,
                Teams = new List<ExtendedTeamDTO>()
            };

            foreach (TeamDB team in db.Teams)
            {
                ExtendedTeamDTO teamDTO = new ExtendedTeamDTO()
                {
                    TeamID = team.IngameID,
                    Win = team.Win,
                    Inhibitors = team.Inhibitors,
                    Towers = team.Towers,
                    Participants = new List<ExtendedParticipantDTO>()
                };
                int assists = 0;
                int kills = 0;
                int deaths = 0;
                foreach (ParticipantDB part in team.Participants)
                {
                    ExtendedParticipantDTO participantDTO = new ExtendedParticipantDTO()
                    {
                        Champion = part.ChampionName,
                        ChampionIcon = null,
                        Icon = null,
                        IsOinky = CheckIfOinky(part.SummonerName),
                        Role = part.Role,
                        SummonerID = part.Puuid,
                        SummonerName = part.SummonerName,
                        Assists = part.Assists,
                        Deaths = part.Deaths,
                        Kills = part.Kills
                    };
                    assists += participantDTO.Assists;
                    kills += participantDTO.Kills;
                    deaths += participantDTO.Deaths;
                    teamDTO.Participants.Add(participantDTO);
                }
                teamDTO.Assists = assists;
                teamDTO.Kills = kills;
                teamDTO.Deaths = deaths;
                result.Teams.Add(teamDTO);
            }

            return result;
        }

        public static MatchDTO ToResultModel(this MatchDB db)
        {
            MatchDTO result = new MatchDTO()
            {
                Duration = db.GameDuration,
                GameStart = db.GameStartTimestamp,
                MatchID = db.MatchID,
                Mode = db.GameMode,
                Teams = new List<TeamDTO>()
            };

            foreach (TeamDB team in db.Teams)
            {
                TeamDTO teamDTO = new TeamDTO()
                {
                    TeamID = team.IngameID,
                    Win = team.Win,
                    Participants = new List<ParticipantDTO>()
                };
                foreach (ParticipantDB participant in team.Participants)
                {
                    ParticipantDTO participantDTO = new ParticipantDTO()
                    {
                        Champion = participant.ChampionName,
                        ChampionIcon = null,
                        Icon = null,
                        IsOinky = CheckIfOinky(participant.SummonerName),
                        Role = participant.Role,
                        SummonerID = participant.Puuid,
                        SummonerName = participant.SummonerName
                    };
                    teamDTO.Participants.Add(participantDTO);
                }
                result.Teams.Add(teamDTO);
            }
            return result;
        }

        public static MatchDTO ToResultModel(this MatchRiotDTO dto)
        {
            MatchDTO resultMatch = new MatchDTO();

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
            resultMatch.Teams = new List<TeamDTO>();
            foreach (TeamRiotDTO team in dto.Info.Teams)
            {
                TeamDTO resultTeam = new TeamDTO()
                {
                    TeamID = team.TeamId,
                    Win = team.Win,
                    Participants = new List<ParticipantDTO>()
                };
                List<ParticipantRiotDTO> participants = dto.Info.Participants.Where(p => p.TeamId == team.TeamId).ToList();
                foreach (ParticipantRiotDTO p in participants)
                {
                    ParticipantDTO participant = new ParticipantDTO()
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

        public static bool CheckIfOinky(string summonerName)
        {
            return APIUtils.OINKIES.Contains(summonerName);
        }

        public static int ConvertRiotMode(int mode)
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
    }
}