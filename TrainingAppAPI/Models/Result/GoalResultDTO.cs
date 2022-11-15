namespace Oinky.TrainingAppAPI.Models.Result
{
    public class GoalResultDTO
    {
        public Guid GoalID { get; set; }
        public string DisplayName { get; set; }
        public string MatchID { get; set; } 
        public List<ParticipantGoalResult> Participants { get; set; }
    }
}
