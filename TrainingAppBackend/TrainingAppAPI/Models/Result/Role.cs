using System.Text.Json.Serialization;

namespace Oinky.TrainingAppAPI.Models.Result
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
