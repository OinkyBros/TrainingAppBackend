namespace Oinky.TrainingAppAPI.Models.DB
{
    public class SummonerDB
    {
        public string DisplayName { get; set; }
        public int ProfileIconId { get; set; }
        public string PUUID { get; set; }
        public long RevisionDate { get; set; }
        public int SummonerLevel { get; set; }
    }
}