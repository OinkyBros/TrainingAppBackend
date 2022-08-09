using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        TOP,
        JUNGLE,
        MID,
        BOT,
        SUPP,
        UNDEFINED
    }
}