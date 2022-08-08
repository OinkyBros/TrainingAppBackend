using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.RiotAPI
{
    public class StyleRiotDTO
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("selections")]
        public List<SelectionRiotDTO> Selections { get; set; }

        [JsonPropertyName("style")]
        public int StyleNr { get; set; }
    }
}