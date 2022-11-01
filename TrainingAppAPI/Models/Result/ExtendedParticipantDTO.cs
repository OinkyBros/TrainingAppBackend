using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.Result
{
    public class ExtendedParticipantDTO : ParticipantDTO
    {
        [JsonPropertyName("Assists")]
        public int Assists { get; set; }

        [JsonPropertyName("Deaths")]
        public int Deaths { get; set; }

        [JsonPropertyName("Kills")]
        public int Kills { get; set; }

        [JsonPropertyName("CS")]
        public int CS { get; set; }

        [JsonPropertyName("Visionscore")]
        public int VisionScore { get; set; }
    }
}