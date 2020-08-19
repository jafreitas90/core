using System;
using System.Collections.Generic;
using CurrencyConverter.Model;
using CurrencyConverter.Services.Models.Results;

namespace CurrencyConverter.Services.Mappings
{
    public static class MappingExtensions
    {
        public static ExchangeRates ToExchangeRates(this ExchangeRatesSynchronizerResult result)
        {
            try
            {
                var rate = new Rate(result.Base, result.Amount);
                var date = result.Date;
                var rates = new List<Rate>();
                foreach (var r in result.Rates)
                {
                    rates.Add(new Rate(r.Key, r.Value));
                }
                rates.Add(new Rate(rate.CurrencyType, rate.Value));
                return new ExchangeRates(rate, date, rates);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException(ex.Message, ex);
            }
        }
    }
}