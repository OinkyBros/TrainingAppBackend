using Oinky.TrainingAppAPI.Models.Enums;
using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.Result
{
    public class MatchDTO
    {
        [JsonPropertyName("Duration")]
        public int Duration { get; set; }

        [JsonPropertyName("MatchID")]
        public string MatchID { get; set; } = null;

        [JsonPropertyName("Mode")]
        public GameMode Mode { get; set; }

        [JsonPropertyName("Teams")]
        public List<TeamDTO> Teams { get; set; } = null;

        [JsonPropertyName("Timestamp")]
        public long GameStart { get; set; }
    }
}