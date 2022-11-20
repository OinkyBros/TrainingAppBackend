using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.Request
{
    public class AddGoalRequest
    {
        [JsonPropertyName("BotGoal")]
        public string BotGoal { get; set; }
        [JsonPropertyName("DisplayName")]
        public string DisplayName { get; set; }
        [JsonPropertyName("JungleGoal")]
        public string JungleGoal { get; set; }
        [JsonPropertyName("MidGoal")]
        public string MidGoal { get; set; }
        [JsonPropertyName("SuppGoal")]
        public string SuppGoal { get; set; }
        [JsonPropertyName("TopGoal")]
        public string TopGoal { get; set; }
    }
}