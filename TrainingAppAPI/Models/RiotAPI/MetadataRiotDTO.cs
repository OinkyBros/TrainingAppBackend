using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.RiotAPI
{
    public class MetadataRiotDTO
    {
        [JsonPropertyName("dataVersion")]
        public string DataVersion { get; set; }

        [JsonPropertyName("matchId")]
        public string MatchId { get; set; }

        [JsonPropertyName("participants")]
        public List<string> Participants { get; set; }
    }
}