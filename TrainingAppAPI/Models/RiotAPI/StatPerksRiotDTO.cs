using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.RiotAPI
{
    public class StatPerksRiotDTO
    {
        [JsonPropertyName("defense")]
        public int Defense { get; set; }

        [JsonPropertyName("flex")]
        public int Flex { get; set; }

        [JsonPropertyName("offense")]
        public int Offense { get; set; }
    }
}
