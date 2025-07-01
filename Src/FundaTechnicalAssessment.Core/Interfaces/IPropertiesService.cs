using FundaTechnicalAssessment.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundaTechnicalAssessment.Core.Interfaces
{
    public interface IPropertiesService
    {
        public Task<IEnumerable<PropertyListingsDto>> GetRankPropertiesByAgentAsync(string city, bool hasGarden);
    }
}
