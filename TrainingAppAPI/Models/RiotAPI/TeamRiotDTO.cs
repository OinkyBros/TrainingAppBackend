using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.RiotAPI
{
    public class TeamRiotDTO
    {
        [JsonPropertyName("bans")]
        public List<BanRiotDTO> Bans { get; set; }

        [JsonPropertyName("objectives")]
        public ObjectivesRiotDTO Objectives { get; set; }

        [JsonPropertyName("teamId")]
        public int TeamId { get; set; }

        [JsonPropertyName("win")]
        public bool Win { get; set; }
    }
}
