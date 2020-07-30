using System.Threading.Tasks;

namespace GrpcService1.Models.Abstract
{
    public interface IFootballApiSource
    {
        Task<CurrentStandingsResponse> GetCurrentStandingsAsync(int leagueId);
    }
}