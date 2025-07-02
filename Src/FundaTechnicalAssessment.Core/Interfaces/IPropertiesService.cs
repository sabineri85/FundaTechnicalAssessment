using FundaTechnicalAssessment.Core.Model;

namespace FundaTechnicalAssessment.Core.Interfaces
{
    public interface IPropertiesService
    {
        public Task<IEnumerable<AgentPropertyGroup>> RankPropertiesByAgentAsync(string city, bool hasGarden);
    }
}
