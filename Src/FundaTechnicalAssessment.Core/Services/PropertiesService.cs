using FundaTechnicalAssessment.Core.Extensions;
using FundaTechnicalAssessment.Core.Interfaces;
using FundaTechnicalAssessment.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundaTechnicalAssessment.Core.Services
{
    public class PropertiesService(IFundaListingsProvider fundaListingsProvider) : IPropertiesService
    {
        private const string _queryBase = "?type=koop&zo=/";
        private const string _gardenParameter = "/tuin";
        public async Task<IEnumerable<PropertyListingsDto>> GetRankPropertiesByAgentAsync(string city, bool hasGarden)
        {
            var pageNumber = 1;
            var builtQuery = AppendQueryParameters(city, hasGarden);
            var propertyObjectList = new List<PropertyListingsDto>();

            while(true)
            {
                var propertyListings = await fundaListingsProvider.SearchPropertiesByParametersAsync(builtQuery, pageNumber);
                
                if(!propertyListings.Any())
                {
                    break;
                }

                propertyObjectList.AddRange(propertyListings);
                pageNumber++;
            }


            return propertyObjectList;
        }

        private string AppendQueryParameters(string city, bool hasGarden)
        {
            var gardenParameter = hasGarden ? _gardenParameter : string.Empty;
            return $"{_queryBase}{city}{gardenParameter}";
        }
    }
}
