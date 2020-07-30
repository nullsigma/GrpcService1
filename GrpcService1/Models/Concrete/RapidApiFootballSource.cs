using GrpcService1.Models.Abstract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using static GrpcService1.CurrentStandingsResponse.Types;

namespace GrpcService1.Models.Concrete
{
    public class RapidApiFootballSource : IFootballApiSource
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly RapidApiFootballConfig _rapidConfig;

        public RapidApiFootballSource(ILogger<GreeterService> logger, IOptions<RapidApiFootballConfig> rapidConfig)
        {
            _logger = logger;
            _rapidConfig = rapidConfig.Value;
        }

        public async Task<CurrentStandingsResponse> GetCurrentStandingsAsync(int leagueId)
        {
            try
            {
                // Creating request         
                var client = new RestClient($"{_rapidConfig.LeagueTableEndpoint}/{leagueId}");
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-rapidapi-host", _rapidConfig.Host);
                request.AddHeader("x-rapidapi-key", _rapidConfig.Key);

                // Sending request
                IRestResponse response = await client.ExecuteAsync(request);
                if (!response.IsSuccessful)
                {
                    _logger.LogError($"Request was not successful: {response.StatusCode}; {response.Content}");
                    return null;
                }

                // Deserializing content
                var model = JsonSerializer.Deserialize<RapidApiStandingsModel>(response.Content);
                if (model?.api?.standings == null || model.api.standings.Length == 0)
                {
                    _logger.LogError($"Could not deserialize json: {response.Content}");
                    return null;
                }

                // Generating reply
                var reply = new CurrentStandingsResponse();
                foreach (var standing in model.api.standings[0])
                {
                    reply.Stangings.Add((Standing)standing);
                }

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured: {ex.Message}");
                return null;
            }
        }
    }
}