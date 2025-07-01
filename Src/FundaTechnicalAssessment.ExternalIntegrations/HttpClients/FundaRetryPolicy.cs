using Polly;
using System.Net;

namespace FundaTechnicalAssessment.ExternalIntegrations.HttpClients
{
    public static class FundaRetryPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRateLimitPolicy()
        {
            return Policy
                .HandleResult<HttpResponseMessage>(r =>
                    r.StatusCode == HttpStatusCode.TooManyRequests ||
                    (r.StatusCode == HttpStatusCode.Unauthorized))
                .WaitAndRetryAsync(
                    3,
                    _ => TimeSpan.FromSeconds(60),
                    onRetry: (outcome, timespan, retryAttempt, context) =>
                    {
                        Console.WriteLine($"Retry {retryAttempt} after {timespan.TotalSeconds} seconds due to {outcome.Result?.StatusCode}");
                    });
        }
    }

}
