using FundaTechnicalAssessment.Core.Model;
using FundaTechnicalAssessment.ExternalIntegrations.Models;

namespace FundaTechnicalAssessment.ExternalIntegrations.Extensions
{
    public static class ResponseMappingExtensions
    {
        public static IEnumerable<PropertyListingsDto> Map(this PropertyDetailsResponse propertyDetailsResponse)
        {            
            return propertyDetailsResponse.Objects.Select(x => new PropertyListingsDto
            {
                AgentId = x.MakelaarId,
                AgentName = x.MakelaarNaam
            });
        }
    }
}
