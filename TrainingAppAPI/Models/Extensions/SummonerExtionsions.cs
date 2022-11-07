using Oinky.TrainingAppAPI.Models.DB;
using Oinky.TrainingAppAPI.Models.RiotAPI;

namespace Oinky.TrainingAppAPI.Models.Extensions
{
    public static class SummonerExtionsions
    {
        public static SummonerDB ToDBModel(this SummonerRiotDTO dto)
        {
            return new SummonerDB()
            {
                DisplayName = dto.Name,
                PUUID = dto.PUUID,
                ProfileIconId = dto.ProfileIconID,
                RevisionDate = dto.RevisionDate,
                SummonerLevel = dto.SummonerLevel
            };
        }
    }
}