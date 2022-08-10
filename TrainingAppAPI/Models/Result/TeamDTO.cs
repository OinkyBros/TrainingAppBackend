using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.Result
{
    public class TeamDTO
    {
        [JsonPropertyName("Participants")]
        public List<ParticipantDTO> Participants { get; set; }

        [JsonPropertyName("TeamID")]
        public int TeamID { get; set; }

        [JsonPropertyName("Win")]
        public bool Win { get; set; }
    }
}