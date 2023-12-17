using Microsoft.Extensions.Options;
using RestSharp;
using SportsHub.Services.DTO;
using SportsHub.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.Services.Services.Realizations
{
    public class RecommendationsRestClient : IRecommendationsRestClient
    {
        private RestClient _restClient;
        private RecommendationSystemAPI ApiSettings;

        public RecommendationsRestClient(IOptions<RecommendationSystemAPI> options)
        {
            ApiSettings = options.Value;
            var clientOptions = new RestClientOptions(ApiSettings.Host)
            {
            };
            _restClient = new RestClient(clientOptions);
        }
        public async Task<List<RecommendationDto>> GetRecommendationsAsync(string userId, int count)
        {
            var request = new RestRequest(ApiSettings.GetEndpoint, Method.Get);
            request.AddQueryParameter("userId", userId);
            request.AddQueryParameter("suggestionsCount", count);

            var response = await _restClient.GetAsync<List<RecommendationDto>>(request);
            return response;
        }

        public async Task<bool> LikeAsync(string userId, int trainingProgramId, int rating)
        {
            var request = new RestRequest(ApiSettings.LikeEndpoint, Method.Get);
            request.AddQueryParameter("userId", userId);
            request.AddQueryParameter("trainingProgramId", trainingProgramId);
            request.AddQueryParameter("rating", rating);

            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                return true;
            }
            return false;
        }

        public async Task<int?> GetRatingAsync(string userId, int trainingProgramId)
        {
            var request = new RestRequest(ApiSettings.GetRatingEndpoint, Method.Get);
            request.AddQueryParameter("userId", userId);
            request.AddQueryParameter("trainingProgramId", trainingProgramId);

            var response = await _restClient.GetAsync<int?>(request);
            return response;
        }
    }
}
