namespace Oinky.TrainingAppAPI.Models.DB
{
    public class TeamDB
    {
        public string Bans { get; set; }
        public int Barons { get; set; }
        public int ChampionKills { get; set; }
        public int Dragons { get; set; }
        public bool FirstBaron { get; set; }
        public bool FirstBlood { get; set; }
        public bool FirstDragon { get; set; }
        public bool FirstHerald { get; set; }
        public bool FirstInhibitors { get; set; }
        public bool FirstTower { get; set; }
        public int Heralds { get; set; }
        public int IngameID { get; set; }
        public int Inhibitors { get; set; }
        public string MatchID { get; set; }
        public List<ParticipantDB> Participants { get; set; }
        public Guid TeamId { get; set; }
        public int Towers { get; set; }
        public bool Win { get; set; }
    }
}