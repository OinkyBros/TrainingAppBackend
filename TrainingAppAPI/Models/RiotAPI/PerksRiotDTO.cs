using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.RiotAPI
{
    public class PerksRiotDTO
    {
        public class Perks
        {
            [JsonPropertyName("statPerks")]
            public StatPerksRiotDTO StatPerks { get; set; }

            [JsonPropertyName("styles")]
            public List<StyleRiotDTO> Styles { get; set; }
        }
    }
}
