using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.Result
{
    public class ExtendedTeamDTO
    {
        [JsonPropertyName("Assists")]
        public int Assists { get; set; }

        [JsonPropertyName("Deaths")]
        public int Deaths { get; set; }

        [JsonPropertyName("Inhibitors")]
        public int Inhibitors { get; set; }

        [JsonPropertyName("Kills")]
        public int Kills { get; set; }

        [JsonPropertyName("Participants")]
        public List<ExtendedParticipantDTO> Participants { get; set; }

        [JsonPropertyName("TeamID")]
        public int TeamID { get; set; }

        [JsonPropertyName("Towers")]
        public int Towers { get; set; }

        [JsonPropertyName("Win")]
        public bool Win { get; set; }
    }
}