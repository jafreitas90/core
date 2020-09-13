using CurrencyConverter.UI.Events;
using CurrencyConverter.UI.Events.Data;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CurrencyConverter.UI.ViewModels
{
    public interface IHistoryViewModel : IViewModelBase
    {
        public void Load(int id);
    }
    public class HistoryViewModel : Observable, IHistoryViewModel
    {
        public ObservableCollection<ExchangedHistory> Items { get; set; }

        private readonly IEventAggregator _eventAggregator;
        public HistoryViewModel(IEventAggregator eventAggregator)
        {
            Items = new ObservableCollection<ExchangedHistory>();
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<HistoryEvent>().Subscribe(OnHistoryReceived);
        }

        public void Load(int id)
        {
            throw new NotImplementedException();
        }

        private void OnHistoryReceived(ExchangedHistory history)
        {
            Items.Add(history);
        }
    }
}
