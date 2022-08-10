using Oinky.TrainingAppAPI.Models.Enums;
using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.Result
{
    public class ExtendedMatchDTO
    {
        [JsonPropertyName("Duration")]
        public int Duration { get; set; }

        [JsonPropertyName("MatchID")]
        public string MatchID { get; set; } = null;

        [JsonPropertyName("Mode")]
        public GameMode Mode { get; set; }

        [JsonPropertyName("Teams")]
        public List<ExtendedTeamDTO> Teams { get; set; } = null;

        [JsonPropertyName("Timestamp")]
        public long GameStart { get; set; }
    }
}
