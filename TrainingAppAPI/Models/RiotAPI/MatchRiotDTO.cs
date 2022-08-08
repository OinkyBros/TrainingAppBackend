using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.RiotAPI
{
    public class MatchRiotDTO
    {
        [JsonPropertyName("info")]
        public InfoRiotDTO Info { get; set; }

        [JsonPropertyName("metadata")]
        public MetadataRiotDTO Metadata { get; set; }
    }
}