using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.RiotAPI
{
    public class ObjectivesRiotDTO
    {
        [JsonPropertyName("baron")]
        public BaronRiotDTO Baron { get; set; }

        [JsonPropertyName("champion")]
        public ChampionRiotDTO Champion { get; set; }

        [JsonPropertyName("dragon")]
        public DragonRiotDTO Dragon { get; set; }

        [JsonPropertyName("inhibitor")]
        public InhibitorRiotDTO Inhibitor { get; set; }

        [JsonPropertyName("riftHerald")]
        public RiftHeraldRiotDTO RiftHerald { get; set; }

        [JsonPropertyName("tower")]
        public TowerRiotDTO Tower { get; set; }
    }
}
