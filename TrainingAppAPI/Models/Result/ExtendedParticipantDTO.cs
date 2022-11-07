using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.Result
{
    public class ExtendedParticipantDTO : ParticipantDTO
    {
        [JsonPropertyName("Assists")]
        public int Assists { get; set; }

        [JsonPropertyName("BaronKills")]
        public int BaronKills { get; set; }

        [JsonPropertyName("BasicPings")]
        public int BasicPings { get; set; }

        [JsonPropertyName("BountyLevel")]
        public int BountyLevel { get; set; }

        [JsonPropertyName("ChampExperience")]
        public int ChampExperience { get; set; }

        [JsonPropertyName("ChampionId")]
        public int ChampionId { get; set; }

        [JsonPropertyName("ChampionName")]
        public string ChampionName { get; set; }

        [JsonPropertyName("ChampionTransform")]
        public int ChampionTransform { get; set; }

        [JsonPropertyName("ChampLevel")]
        public int ChampLevel { get; set; }

        [JsonPropertyName("ConsumablesPurchased")]
        public int ConsumablesPurchased { get; set; }

        [JsonPropertyName("CS")]
        public int CS { get; set; }

        [JsonPropertyName("DamageDealtToBuildings")]
        public int DamageDealtToBuildings { get; set; }

        [JsonPropertyName("DamageDealtToObjectives")]
        public int DamageDealtToObjectives { get; set; }

        [JsonPropertyName("DamageDealtToTurrets")]
        public int DamageDealtToTurrets { get; set; }

        [JsonPropertyName("DamageSelfMitigated")]
        public int DamageSelfMitigated { get; set; }

        [JsonPropertyName("DamageShare")]
        public double DamageShare { get; set; }

        [JsonPropertyName("Deaths")]
        public int Deaths { get; set; }

        [JsonPropertyName("DetectorWardsPlaced")]
        public int DetectorWardsPlaced { get; set; }

        [JsonPropertyName("DoubleKills")]
        public int DoubleKills { get; set; }

        [JsonPropertyName("DragonKills")]
        public int DragonKills { get; set; }

        [JsonPropertyName("EligibleForProgression")]
        public bool EligibleForProgression { get; set; }

        [JsonPropertyName("FirstBloodAssist")]
        public bool FirstBloodAssist { get; set; }

        [JsonPropertyName("FirstBloodKill")]
        public bool FirstBloodKill { get; set; }

        [JsonPropertyName("FirstTowerAssist")]
        public bool FirstTowerAssist { get; set; }

        [JsonPropertyName("FirstTowerKill")]
        public bool FirstTowerKill { get; set; }

        [JsonPropertyName("GameEndedInEarlySurrender")]
        public bool GameEndedInEarlySurrender { get; set; }

        [JsonPropertyName("GameEndedInSurrender")]
        public bool GameEndedInSurrender { get; set; }

        [JsonPropertyName("GoldEarned")]
        public int GoldEarned { get; set; }

        [JsonPropertyName("GoldShare")]
        public double GoldShare { get; set; }

        [JsonPropertyName("GoldSpent")]
        public int GoldSpent { get; set; }

        [JsonPropertyName("InhibitorKills")]
        public int InhibitorKills { get; set; }

        [JsonPropertyName("InhibitorsLost")]
        public int InhibitorsLost { get; set; }

        [JsonPropertyName("InhibitorTakedowns")]
        public int InhibitorTakedowns { get; set; }

        [JsonPropertyName("Item0")]
        public int Item0 { get; set; }

        [JsonPropertyName("Item1")]
        public int Item1 { get; set; }

        [JsonPropertyName("Item2")]
        public int Item2 { get; set; }

        [JsonPropertyName("Item3")]
        public int Item3 { get; set; }

        [JsonPropertyName("Item4")]
        public int Item4 { get; set; }

        [JsonPropertyName("Item5")]
        public int Item5 { get; set; }

        [JsonPropertyName("Item6")]
        public int Item6 { get; set; }

        [JsonPropertyName("ItemsPurchased")]
        public int ItemsPurchased { get; set; }

        [JsonPropertyName("KillingSprees")]
        public int KillingSprees { get; set; }

        [JsonPropertyName("Kills")]
        public int Kills { get; set; }

        [JsonPropertyName("LargestCriticalStrike")]
        public int LargestCriticalStrike { get; set; }

        [JsonPropertyName("LargestKillingSpree")]
        public int LargestKillingSpree { get; set; }

        [JsonPropertyName("LargestMultiKill")]
        public int LargestMultiKill { get; set; }

        [JsonPropertyName("LongestTimeSpentLiving")]
        public int LongestTimeSpentLiving { get; set; }

        [JsonPropertyName("MagicDamageDealt")]
        public int MagicDamageDealt { get; set; }

        [JsonPropertyName("MagicDamageDealtToChampions")]
        public int MagicDamageDealtToChampions { get; set; }

        [JsonPropertyName("MagicDamageTaken")]
        public int MagicDamageTaken { get; set; }

        [JsonPropertyName("NeutralMinionsKilled")]
        public int NeutralMinionsKilled { get; set; }

        [JsonPropertyName("NexusKills")]
        public int NexusKills { get; set; }

        [JsonPropertyName("NexusLost")]
        public int NexusLost { get; set; }

        [JsonPropertyName("NexusTakedowns")]
        public int NexusTakedowns { get; set; }

        [JsonPropertyName("ObjectivesStolen")]
        public int ObjectivesStolen { get; set; }

        [JsonPropertyName("ObjectivesStolenAssists")]
        public int ObjectivesStolenAssists { get; set; }

        [JsonPropertyName("ParticipantId")]
        public int ParticipantId { get; set; }

        [JsonPropertyName("PentaKills")]
        public int PentaKills { get; set; }

        [JsonPropertyName("PhysicalDamageDealt")]
        public int PhysicalDamageDealt { get; set; }

        [JsonPropertyName("PhysicalDamageDealtToChampions")]
        public int PhysicalDamageDealtToChampions { get; set; }

        [JsonPropertyName("PhysicalDamageTaken")]
        public int PhysicalDamageTaken { get; set; }

        [JsonPropertyName("ProfileIcon")]
        public int ProfileIcon { get; set; }

        [JsonPropertyName("Puuid")]
        public string Puuid { get; set; }

        [JsonPropertyName("QuadraKills")]
        public int QuadraKills { get; set; }

        [JsonPropertyName("RiotIdName")]
        public string RiotIdName { get; set; }

        [JsonPropertyName("RiotIdTagline")]
        public string RiotIdTagline { get; set; }

        [JsonPropertyName("SightWardsBoughtInGame")]
        public int SightWardsBoughtInGame { get; set; }

        [JsonPropertyName("Spell1Casts")]
        public int Spell1Casts { get; set; }

        [JsonPropertyName("Spell2Casts")]
        public int Spell2Casts { get; set; }

        [JsonPropertyName("Spell3Casts")]
        public int Spell3Casts { get; set; }

        [JsonPropertyName("Spell4Casts")]
        public int Spell4Casts { get; set; }

        [JsonPropertyName("Summoner1Casts")]
        public int Summoner1Casts { get; set; }

        [JsonPropertyName("Summoner1Id")]
        public int Summoner1Id { get; set; }

        [JsonPropertyName("Summoner2Casts")]
        public int Summoner2Casts { get; set; }

        [JsonPropertyName("Summoner2Id")]
        public int Summoner2Id { get; set; }

        [JsonPropertyName("SummonerLevel")]
        public int SummonerLevel { get; set; }

        [JsonPropertyName("TeamEarlySurrendered")]
        public bool TeamEarlySurrendered { get; set; }

        [JsonPropertyName("TimeCCingOthers")]
        public int TimeCCingOthers { get; set; }

        [JsonPropertyName("TimePlayed")]
        public int TimePlayed { get; set; }

        [JsonPropertyName("TotalDamageDealt")]
        public int TotalDamageDealt { get; set; }

        [JsonPropertyName("TotalDamageDealtToChampions")]
        public int TotalDamageDealtToChampions { get; set; }

        [JsonPropertyName("TotalDamageShieldedOnTeammates")]
        public int TotalDamageShieldedOnTeammates { get; set; }

        [JsonPropertyName("TotalDamageTaken")]
        public int TotalDamageTaken { get; set; }

        [JsonPropertyName("TotalHeal")]
        public int TotalHeal { get; set; }

        [JsonPropertyName("TotalHealsOnTeammates")]
        public int TotalHealsOnTeammates { get; set; }

        [JsonPropertyName("TotalMinionsKilled")]
        public int TotalMinionsKilled { get; set; }

        [JsonPropertyName("TotalTimeCCDealt")]
        public int TotalTimeCCDealt { get; set; }

        [JsonPropertyName("TotalTimeSpentDead")]
        public int TotalTimeSpentDead { get; set; }

        [JsonPropertyName("TotalUnitsHealed")]
        public int TotalUnitsHealed { get; set; }

        [JsonPropertyName("TowerKills")]
        public int TowerKills { get; set; }

        [JsonPropertyName("TowersLost")]
        public int TowersLost { get; set; }

        [JsonPropertyName("TowerTakedowns")]
        public int TowerTakedowns { get; set; }

        [JsonPropertyName("TripleKills")]
        public int TripleKills { get; set; }

        [JsonPropertyName("TrueDamageDealt")]
        public int TrueDamageDealt { get; set; }

        [JsonPropertyName("TrueDamageDealtToChampions")]
        public int TrueDamageDealtToChampions { get; set; }

        [JsonPropertyName("TrueDamageTaken")]
        public int TrueDamageTaken { get; set; }

        [JsonPropertyName("UnrealKills")]
        public int UnrealKills { get; set; }

        [JsonPropertyName("VisionScore")]
        public int VisionScore { get; set; }

        [JsonPropertyName("VisionWardsBoughtInGame")]
        public int VisionWardsBoughtInGame { get; set; }

        [JsonPropertyName("WardsKilled")]
        public int WardsKilled { get; set; }

        [JsonPropertyName("WardsPlaced")]
        public int WardsPlaced { get; set; }
    }
}