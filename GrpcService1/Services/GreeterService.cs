using System.Threading.Tasks;
using Grpc.Core;
using GrpcService1.Models.Abstract;
using Microsoft.Extensions.Logging;

namespace GrpcService1
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly IFootballApiSource _footballApi;

        public GreeterService(ILogger<GreeterService> logger, IFootballApiSource footballApi)
        {
            _logger = logger;
            _footballApi = footballApi;
        }

        public override Task<CurrentStandingsResponse> GetCurrentStandings(CurrentStandingsRequest request, ServerCallContext context)
        {
            return _footballApi.GetCurrentStandingsAsync(request.LeagueId);
        }
    }
}