using FundaTechnicalAssessment.Core.Interfaces;
using FundaTechnicalAssessment.Core.Model;
using FundaTechnicalAssessment.ExternalIntegrations.Extensions;
using FundaTechnicalAssessment.ExternalIntegrations.Models;
using FundaTechnicalAssessment.ExternalIntegrations.Models.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace FundaTechnicalAssessment.ExternalIntegrations.HttpClients
{
    public class FundaHttpClient : IFundaListingsProvider
    {
        private const string _maxPageSize = "&pagesize=25";
        private readonly HttpClient _httpClient;
        private readonly PropertySearchSettings _propertySearchSettings;
        private readonly ILogger _logger;
        public FundaHttpClient(HttpClient httpClient, IOptions<PropertySearchSettings> propertySearchSettings, ILogger<FundaHttpClient> logger)
        {
            _httpClient = httpClient;
            _propertySearchSettings = propertySearchSettings.Value;
            _logger = logger;

            if (string.IsNullOrEmpty(_propertySearchSettings.BaseUrl))
            {
                throw new ArgumentException($"PropertSearchSettings.BaseUrl must have a value");
            }
            if (string.IsNullOrEmpty(_propertySearchSettings.ApiKey))
            {
                throw new ArgumentException($"PropertSearchSettings.ApiKey must have a value");
            }

            _httpClient.BaseAddress = new Uri(_propertySearchSettings.BaseUrl);
        }

        public async Task<IEnumerable<PropertyListingsDto>> SearchPropertiesByParametersAsync(string builtParameters, int pageNumber)
        {
            // Polly will handle retries, including unauthorised from rate limits
            var policy = FundaRetryPolicy.GetRateLimitPolicy();
            var escapedUri = Uri.EscapeDataString(BuildQuery(builtParameters, pageNumber));
            var requestUri = new Uri(BuildRequestUrl(_propertySearchSettings.BaseUrl, _propertySearchSettings.ApiKey, escapedUri));
           
            var response = await policy.ExecuteAsync(() => _httpClient.SendAsync(new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = requestUri
            }));
                
            var content = await response.Content.ReadAsStringAsync();
            if(!response.IsSuccessStatusCode)
            {
                _logger.LogError("Error retrieving properties when calling API. HttpStatusCode = {statusCode}  ResponseBody = {responseBody}",
                    response.StatusCode,
                    content);
                // we want this to return the correct result or nothing. If there is a failure outside of rate limits
                // fail the whole thing.
                response.StatusCode.ThrowExceptionOnFailedCall();
            }
            var deserealisedResult = JsonSerializer.Deserialize<PropertyDetailsResponse>(content);
            if (deserealisedResult is null)
                throw new InvalidOperationException("Failed to deserealise response");

            return deserealisedResult.Map();
        }

        private string BuildQuery(string queryParameters, int page) =>
            $"{queryParameters}&page={page}{_maxPageSize}";

        private string BuildRequestUrl(string baseUrl, string apiKey, string escapedQueryUrl) =>
            $"{baseUrl}{apiKey}{escapedQueryUrl}";
    }
}
