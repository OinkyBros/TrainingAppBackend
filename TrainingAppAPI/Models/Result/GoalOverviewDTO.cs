namespace Oinky.TrainingAppAPI.Models.Result
{
    public class GoalOverviewDTO
    {
        public List<SingleGoalDTO> CustomGoals { get; set; } =  new List<SingleGoalDTO>();
        public List<SingleGoalDTO> DefaultGoals { get; set; } = new List<SingleGoalDTO>();
    }
}