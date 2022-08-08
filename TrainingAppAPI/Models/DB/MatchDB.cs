using Oinky.TrainingAppAPI.Models.Result;

namespace Oinky.TrainingAppAPI.Models.DB
{
    public class MatchDB
    {
        public long GameCreation { get; set; }
        public int GameDuration { get; set; }
        public long GameEndTimestamp { get; set; }
        public GameMode GameMode { get; set; }
        public string GameName { get; set; }
        public long GameStartTimestamp { get; set; }
        public string MatchID { get; set; }
        public List<TeamDB> Teams { get; set; }
    }
}