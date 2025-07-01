using Newtonsoft.Json;

namespace FundaTechnicalAssessment.ExternalIntegrations.Models
{
    public class PropertyDetailsResponse
    {
        public IEnumerable<Objects> Objects { get; set; } = [];
    }

    public class Objects
    {
        public int MakelaarId { get; set; }
        public string? MakelaarNaam { get; set; }
    }
}
