using CurrencyConverter.UI.Events.Data;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverter.UI.Events
{
    public class HistoryEvent : PubSubEvent<ExchangedHistory>
    {
    }
}
