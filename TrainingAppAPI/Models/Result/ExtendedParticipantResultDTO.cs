using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.Result
{
    public class ExtendedParticipantResultDTO : ParticipantResultDTO
    {
        [JsonPropertyName("Assists")]
        public int Assists { get; set; }

        [JsonPropertyName("Deaths")]
        public int Deaths { get; set; }

        [JsonPropertyName("Kills")]
        public int Kills { get; set; }
    }
}