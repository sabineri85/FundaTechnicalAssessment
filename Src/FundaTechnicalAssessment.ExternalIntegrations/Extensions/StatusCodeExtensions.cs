using System.Net;

namespace FundaTechnicalAssessment.ExternalIntegrations.Extensions
{
    internal static class StatusCodeExtensions
    {
        public static void ThrowExceptionOnFailedCall(this HttpStatusCode httpStatusCode) 
        {
            switch (httpStatusCode)
            {
                case HttpStatusCode code when ((int)code >= 400 && (int)code < 500):
                    throw new InvalidOperationException($"Client error: {(int)code} {code}");

                case HttpStatusCode code when ((int)code >= 500 && (int)code < 600):
                    throw new Exception($"Server error: {(int)code} {code}");

                case HttpStatusCode code when ((int)code == 401):
                    throw new UnauthorizedAccessException("Unauthorized access.");

                default:
                    // For all other status codes, maybe no exception or a general one
                    Console.WriteLine($"Status code {(int)httpStatusCode} handled normally.");
                    break;
            }
        }
        
    }
}
