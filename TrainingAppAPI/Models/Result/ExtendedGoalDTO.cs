namespace Oinky.TrainingAppAPI.Models.Result
{
    public class ExtendedGoalDTO : SingleGoalDTO
    {

        public string BotGoal { get; set; }
        public string JungleGoal { get; set; }
        public string MidGoal { get; set; }
        public string SuppGoal { get; set; }
        public string TopGoal { get; set; }
    }
}
