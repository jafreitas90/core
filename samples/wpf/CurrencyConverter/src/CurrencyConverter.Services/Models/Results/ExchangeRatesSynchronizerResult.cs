using System.Collections.Generic;

namespace CurrencyConverter.Services.Models.Results
{
    public class ExchangeRatesSynchronizerResult
    {
        public decimal Amount { get; set; }
        public string Base { get; set; }
        public string Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }
}