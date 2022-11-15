using Oinky.TrainingAppAPI.Models.Enums;

namespace Oinky.TrainingAppAPI.Models.Result
{
    public class ParticipantGoalResult
    {
        public double GoalResult { get; set; }
        public bool IsOinky { get; set; }
        public string PUUID { get; set; }
        public Role Role { get; set; }
        public string SummonerName { get; set; }
    }
}