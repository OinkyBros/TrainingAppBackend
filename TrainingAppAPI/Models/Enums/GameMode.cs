using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GameMode
    {
        NORMAL,
        DUO,
        FLEX,
        CLASH
    }
}