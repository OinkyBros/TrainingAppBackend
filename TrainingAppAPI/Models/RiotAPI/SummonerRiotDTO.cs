using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.RiotAPI
{
    public class SummonerRiotDTO
    {
        [JsonPropertyName("accountId")]
        public string AccountID { get; set; }

        [JsonPropertyName("id")]
        public string ID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("profileIconId")]
        public int ProfileIconID { get; set; }

        [JsonPropertyName("puuid")]
        public string PUUID { get; set; }

        [JsonPropertyName("revisionDate")]
        public long RevisionDate { get; set; }

        [JsonPropertyName("summonerLevel")]
        public int SummonerLevel { get; set; }
    }
}