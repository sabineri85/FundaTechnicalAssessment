using System.Net;

namespace FundaTechnicalAssessment.ExternalIntegrations.Extensions
{
    public static class StatusCodeExtensions
    {
        public static void ThrowExceptionOnFailedCall(this HttpStatusCode httpStatusCode) 
        {
            switch (httpStatusCode)
            {
                case HttpStatusCode code when ((int)code >= 400 && (int)code < 500):
                    throw new InvalidOperationException($"Client error: {(int)code} {code}");

                case HttpStatusCode code when ((int)code >= 500 && (int)code < 600):
                    throw new Exception($"Server error: {(int)code} {code}");

                default:
                    Console.WriteLine($"Status code {(int)httpStatusCode} handled normally.");
                    break;
            }
        }
        
    }
}
