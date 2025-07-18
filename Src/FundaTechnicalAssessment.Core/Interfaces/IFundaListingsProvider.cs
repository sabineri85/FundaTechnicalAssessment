﻿using FundaTechnicalAssessment.Core.Model;

namespace FundaTechnicalAssessment.Core.Interfaces
{
    public interface IFundaListingsProvider
    {
        Task<IEnumerable<PropertyListingsDto>> SearchPropertiesByParametersAsync(string builtQuery, int pageNumber);
    }
}
