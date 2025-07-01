using FundaTechnicalAssessment.Core.Model;

namespace FundaTechnicalAssessment.ExternalIntegrations.Models
{
    public class PropertyListingsResult
    {
        public IEnumerable<PropertyListingsDto> Listings { get; set; } = [];
        public bool ShouldRetry { get; set; } = false;
        public bool IsFatalError { get; set; } = false;
    }
}
