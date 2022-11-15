namespace Oinky.TrainingAppAPI.Models.DB
{
    public class GoalDB
    {
        public string BotGoal { get; set; }
        public string DisplayName { get; set; }
        public Guid GoalID { get; set; }
        public string JungleGoal { get; set; }
        public string MidGoal { get; set; }
        public string SuppGoal { get; set; }
        public string TopGoal { get; set; }
    }
}