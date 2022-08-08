using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.RiotAPI
{
    public class InfoRiotDTO
    {
        [JsonPropertyName("gameCreation")]
        public long GameCreation { get; set; }

        [JsonPropertyName("gameDuration")]
        public int GameDuration { get; set; }

        [JsonPropertyName("gameEndTimestamp")]
        public long GameEndTimestamp { get; set; }

        [JsonPropertyName("gameId")]
        public long GameId { get; set; }

        [JsonPropertyName("gameMode")]
        public string GameMode { get; set; }

        [JsonPropertyName("gameName")]
        public string GameName { get; set; }

        [JsonPropertyName("gameStartTimestamp")]
        public long GameStartTimestamp { get; set; }

        [JsonPropertyName("gameType")]
        public string GameType { get; set; }

        [JsonPropertyName("gameVersion")]
        public string GameVersion { get; set; }

        [JsonPropertyName("mapId")]
        public int MapId { get; set; }

        [JsonPropertyName("participants")]
        public List<ParticipantRiotDTO> Participants { get; set; }

        [JsonPropertyName("platformId")]
        public string PlatformId { get; set; }

        [JsonPropertyName("queueId")]
        public int QueueId { get; set; }

        [JsonPropertyName("teams")]
        public List<TeamRiotDTO> Teams { get; set; }

        [JsonPropertyName("tournamentCode")]
        public string TournamentCode { get; set; }
    }
}