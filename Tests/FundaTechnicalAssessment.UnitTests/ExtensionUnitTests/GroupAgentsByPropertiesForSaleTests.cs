using FundaTechnicalAssessment.Core.Model;
using FundaTechnicalAssessment.Core.Extensions;
using Xunit;
namespace FundaTechnicalAssessment.UnitTests.ExtensionUnitTests
{
    public class GroupAgentsByPropertiesForSaleTests
    {
        [Fact]
        public void GroupsAgentsAndCountsPropertiesCorrectly()
        {
            // Arrange
            var listings = new List<PropertyListingsDto>
        {
            new() { AgentId = 1, AgentName = "Agent A" },
            new() { AgentId = 1, AgentName = "Agent A" },
            new() { AgentId = 2, AgentName = "Agent B" },
            new() { AgentId = 3, AgentName = "Agent C" },
            new() { AgentId = 3, AgentName = "Agent C" },
            new() { AgentId = 3, AgentName = "Agent C" }
        };

            // Act
            var result = listings.GroupAgentsByPropertiesForSale().ToList();

            // Assert
            Assert.Equal(3, result.Count);

            Assert.Equal(3, result[0].PropertyCount); // Agent C
            Assert.Equal("Agent C", result[0].AgentName);

            Assert.Equal(2, result[1].PropertyCount); // Agent A
            Assert.Equal(1, result[2].PropertyCount); // Agent B
        }

        [Fact]
        public void ReturnsTop10Agents_WhenMoreThan10()
        {
            // Arrange
            var listings = new List<PropertyListingsDto>();
            for (int i = 1; i <= 15; i++)
            {
                listings.Add(new PropertyListingsDto
                {
                    AgentId = i,
                    AgentName = $"Agent {i}"
                });
            }

            // Act
            var result = listings.GroupAgentsByPropertiesForSale().ToList();

            // Assert
            Assert.Equal(10, result.Count);
        }

        [Fact]
        public void ReturnsEmptyList_WhenInputIsEmpty()
        {
            // Arrange
            var listings = new List<PropertyListingsDto>();

            // Act
            var result = listings.GroupAgentsByPropertiesForSale();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void HandlesNullAgentName()
        {
            // Arrange
            var listings = new List<PropertyListingsDto>
        {
            new() { AgentId = 1, AgentName = null }
        };

            // Act
            var result = listings.GroupAgentsByPropertiesForSale().First();

            // Assert
            Assert.Equal(string.Empty, result.AgentName);
        }
    }
}
