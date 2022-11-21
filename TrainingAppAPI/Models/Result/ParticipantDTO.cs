using Oinky.TrainingAppAPI.Models.Enums;
using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.Result
{
    public class ParticipantDTO
    {
        [JsonPropertyName("Champion")]
        public string Champion { get; set; } = null;

        [JsonPropertyName("Icon")]
        public int Icon { get; set; }

        [JsonPropertyName("IsOinky")]
        public bool IsOinky { get; set; }

        [JsonPropertyName("Role")]
        public Role Role { get; set; }

        [JsonPropertyName("SummonerID")]
        public string SummonerID { get; set; } = null;

        [JsonPropertyName("SummonerName")]
        public string SummonerName { get; set; } = null;
    }
}