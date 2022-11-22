using Oinky.TrainingAppAPI.Models.Enums;

namespace Oinky.TrainingAppAPI.Models.Configuration
{
    public class APISettings
    {
        public List<GameMode> Modes { get; set; } = new List<GameMode>();
        public List<string> Oinkies { get; set; } = new List<string>();
        public long StartTimestamp { get; set; } = 1659304800;
    }
}