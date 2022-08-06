using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.RiotAPI
{
    public class BanRiotDTO
    {
        [JsonPropertyName("championId")]
        public int ChampionId { get; set; }

        [JsonPropertyName("pickTurn")]
        public int PickTurn { get; set; }
    }
}