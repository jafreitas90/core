using CurrencyConverter.UI.Events;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverter.UI.ViewModels
{
    public class HistoryViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        public HistoryViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<HistoryEvent>().Subscribe(OnHistoryReceived);
        }

        private void OnHistoryReceived()
        {

        }
    }
}
