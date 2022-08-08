using Oinky.TrainingAppAPI.Models.Result;

namespace Oinky.TrainingAppAPI.Services.Interfaces
{
    public interface IMatchService
    {
        Task<List<Match>> GetMatchesAsync();
    }
}
