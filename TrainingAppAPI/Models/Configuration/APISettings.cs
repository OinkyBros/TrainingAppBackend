namespace Oinky.TrainingAppAPI.Models.Configuration
{
    public class APISettings
    {
        public List<string> Oinkies { get; set; } = new List<string>();
        public long StartingTimestamp { get; set; } = 1659304800;
    }
}