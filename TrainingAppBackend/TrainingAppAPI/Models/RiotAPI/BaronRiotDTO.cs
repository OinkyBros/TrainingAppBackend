using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.RiotAPI
{
    public class BaronRiotDTO
    {
        [JsonPropertyName("first")]
        public bool First { get; set; }

        [JsonPropertyName("kills")]
        public int Kills { get; set; }
    }
}