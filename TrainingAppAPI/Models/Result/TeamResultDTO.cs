using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.Result
{
    public class TeamResultDTO
    {
        [JsonPropertyName("Participants")]
        public List<ParticipantResultDTO> Participants { get; set; }

        [JsonPropertyName("TeamID")]
        public int TeamID { get; set; }

        [JsonPropertyName("Win")]
        public bool Win { get; set; }
    }
}