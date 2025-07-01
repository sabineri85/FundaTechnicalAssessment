using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundaTechnicalAssessment.Core.Extensions
{
    public static class QueryStringExtensions
    {
        private const string _queryBase = "?type=koop&zo=/";
        private const string _gardenParameter = "/tuin";
        public static string AppendQueryParameters(this StringBuilder sb, string city, bool hasGarden)
        {
            var gardenParameter = hasGarden ? _gardenParameter : string.Empty;
            return $"{_queryBase}{city}{gardenParameter}";
        }
    }
}
