using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverter.UI.Events
{
    public class HistoryEvent : PubSubEvent
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Result { get; set; }
    }
}
