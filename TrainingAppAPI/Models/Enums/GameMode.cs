using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GameMode
    {
        CUSTOM,
        NORMAL,
        DUO,
        FLEX,
        CLASH,
        ARAM
    }
}