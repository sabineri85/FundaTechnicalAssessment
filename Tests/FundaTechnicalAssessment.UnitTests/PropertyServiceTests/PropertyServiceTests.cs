using FundaTechnicalAssessment.Core.Interfaces;
using FundaTechnicalAssessment.Core.Model;
using FundaTechnicalAssessment.Core.Services;
using NSubstitute;
using Xunit;

namespace FundaTechnicalAssessment.UnitTests.PropertyServiceTests
{
    public class PropertyServiceTests
    {
        private readonly IFundaListingsProvider _provider;
        private readonly IPropertiesService _service; // Use the interface here

        public PropertyServiceTests()
        {
            _provider = Substitute.For<IFundaListingsProvider>();
            _service = new PropertiesService(_provider);
        }

        [Fact]
        public async Task GetRankPropertiesByAgentAsync_ReturnsGroupedAgents()
        {
            var listingsPage1 = new List<PropertyListingsDto>
        {
            new PropertyListingsDto { AgentId = 1, AgentName = "Agent A" },
            new PropertyListingsDto { AgentId = 1, AgentName = "Agent A" },
            new PropertyListingsDto { AgentId = 2, AgentName = "Agent B" },
        };
            var listingsPage2 = new List<PropertyListingsDto>(); // end of pagination

            _provider.SearchPropertiesByParametersAsync(Arg.Any<string>(), 1).Returns(listingsPage1);
            _provider.SearchPropertiesByParametersAsync(Arg.Any<string>(), 2).Returns(listingsPage2);

            var result = await _service.RankPropertiesByAgentAsync("amsterdam", false);

            var grouped = result.ToList();
            Assert.Equal(2, grouped.Count);
            Assert.Contains(grouped, g => g.AgentId == 1 && g.PropertyCount == 2);
            Assert.Contains(grouped, g => g.AgentId == 2 && g.PropertyCount == 1);
        }

        [Theory]
        [InlineData(true, "?type=koop&zo=/amsterdam/tuin")]
        [InlineData(false, "?type=koop&zo=/amsterdam")]
        public async Task GetRankPropertiesByAgentAsync_BuildsCorrectQuery(bool hasGarden, string expectedQuery)
        {
            _provider.SearchPropertiesByParametersAsync(expectedQuery, 1).Returns(new List<PropertyListingsDto>());

            await _service.RankPropertiesByAgentAsync("amsterdam", hasGarden);

            await _provider.Received(1).SearchPropertiesByParametersAsync(expectedQuery, 1);
        }

        [Fact]
        public async Task GetRankPropertiesByAgentAsync_StopsWhenNoMoreResults()
        {
            _provider.SearchPropertiesByParametersAsync(Arg.Any<string>(), 1).Returns(new List<PropertyListingsDto>());

            var result = await _service.RankPropertiesByAgentAsync("utrecht", false);

            Assert.Empty(result);
            await _provider.Received(1).SearchPropertiesByParametersAsync(Arg.Any<string>(), 1);
        }
    }

}
