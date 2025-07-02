using FundaTechnicalAssessment.Core.Model;

namespace FundaTechnicalAssessment.Core.Extensions
{
    public static class PropertyListingExtensions
    {
        private const int _topRankingNumber = 10;
        public static IEnumerable<AgentPropertyGroup> GroupAgentsByPropertiesForSale(this IEnumerable<PropertyListingsDto> propertyListingsDto)
        {
            return propertyListingsDto
                .GroupBy(x => new { x.AgentId, x.AgentName })                
                .Select(y => new AgentPropertyGroup
                {
                    AgentId = y.Key.AgentId,
                    AgentName = y.Key.AgentName is null ? string.Empty : y.Key.AgentName,
                    PropertyCount = y.Count()
                })
                .OrderByDescending(z => z.PropertyCount)
                .Take(_topRankingNumber);                
        }
    }
}
