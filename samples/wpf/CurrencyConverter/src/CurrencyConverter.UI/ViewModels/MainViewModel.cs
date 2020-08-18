﻿using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AsyncAwaitBestPractices.MVVM;
using CurrencyConverter.Model;
using CurrencyConverter.Services.Synchronizers;
using CurrencyConverter.UI.DataProvider;
using CurrencyConverter.UI.DataProvider.Lookups;
using CurrencyConverter.UI.Utilities.Settings;
using CurrencyConverter.UI.Wrapper;
using CurrencyConverter.UI.Utilities;

namespace CurrencyConverter.UI.ViewModels
{
    public class MainViewModel : Observable
    {
        public bool _isBusy;
        public bool _isDataSynchronized;
        private ISynchronizer _synchronizer;

        private ILookupProvider<Rate> _currencyTypeLookupProvider;
        private ExchangeRatesWrappper _exchangeRates;
        private CurrencyWrapper _selectedFromCurrency;
        private CurrencyWrapper _selectedToCurrency;
        private IEnumerable<LookupItem> _currencyTypeGroupLookup;
        private IExchangeRateDataProvider _exchangeRateDataProvider;
        private float _from;

        #region Commands
        public ICommand SyncCommand { get; private set; }
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsDataSynchronized
        {
            get => _isDataSynchronized;
            set
            {
                if (_isDataSynchronized != value)
                {
                    _isDataSynchronized = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public float From
        {
            get { return _from; }
            set
            {
                if (_from != value)
                {
                    _from = value;
                    OnPropertyChanged();
                }
            }
        }

        public ExchangeRatesWrappper ExchangeRates
        {
            get { return _exchangeRates; }
            private set
            {
                _exchangeRates = value;
                OnPropertyChanged();
            }
        }

        public CurrencyWrapper SelectedToCurrency
        {
            get { return _selectedToCurrency; }
            set
            {
                _selectedToCurrency = value;
                OnPropertyChanged();
            }
        }

        public CurrencyWrapper SelectedFromCurrency
        {
            get { return _selectedFromCurrency; }
            set
            {
                _selectedFromCurrency = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<LookupItem> CurrencyTypeGroupLookup
        {
            get { return _currencyTypeGroupLookup; }
            set
            {
                _currencyTypeGroupLookup = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(ISynchronizer synchronizer, ILookupProvider<Rate> currencyTypeLookupProvider, IExchangeRateDataProvider exchangeRateDataProvider)
        {
            _exchangeRateDataProvider = exchangeRateDataProvider;
            _currencyTypeLookupProvider = currencyTypeLookupProvider;
            _synchronizer = synchronizer;
            SyncCommand = new AsyncCommand(canExecute: _ => !IsBusy, execute: OnSyncExecute, onException: OnSyncException);

            // load data
            Task.Run(async () => await LoadAsync()).GetAwaiter().GetResult();
        }

        private void ExchangeFromToCurrency()
        {
            CalculeExchangeRate(SelectedFromCurrency, SelectedToCurrency);
        }

        private void ExchangeToFromCurrency()
        {
            CalculeExchangeRate(SelectedToCurrency, SelectedFromCurrency);
        }

        private void CalculeExchangeRate(CurrencyWrapper from, CurrencyWrapper to)
        {
            var fromCurrencyType = from.CurrencyType;
            var toCurrencyType = to.CurrencyType;
            var amount = from.Value;

            to.Value = ExchangeRates.Model.GetExchangeRate(fromCurrencyType, toCurrencyType, amount);
        }

        private async Task LoadAsync()
        {
            // Get currencies types to lookup
            CurrencyTypeGroupLookup = await _currencyTypeLookupProvider.GetLookupAsync();

            // if no data
            if(CurrencyTypeGroupLookup == null)
            {
                IsDataSynchronized = false;
            }
            else
            {
                IsDataSynchronized = true;

                // Get all exchange rates
                var exchangeRates = await _exchangeRateDataProvider.GetAllDataAsync();
                ExchangeRates = new ExchangeRatesWrappper(exchangeRates);

                if (SelectedToCurrency == null || SelectedFromCurrency == null)
                {
                    SelectedToCurrency = new CurrencyWrapper(new Rate(ExchangeRates.Base.CurrencyType, 1));
                    SelectedToCurrency.CurrencyChangedEvent += ExchangeToFromCurrency;

                    SelectedFromCurrency = new CurrencyWrapper(new Rate(ExchangeRates.Base.CurrencyType, 1));
                    SelectedFromCurrency.CurrencyChangedEvent += ExchangeFromToCurrency;
                    LoadSettings();
                }
                ExchangeFromToCurrency();
            }
        }

        private async Task OnSyncExecute()
        {
            try
            {
                IsBusy = true;
                await _synchronizer.RetrieveAsync();
                await LoadAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void OnSyncException(Exception ex)
        {
            // show some message, write to logs..
            MessageBox.Show("Something went wrong. Contact the system administration please.");
        }

        internal void SaveSettings()
        {
            if(SelectedFromCurrency != null && SelectedToCurrency != null)
            {
                SettingsService.AddValue(Constants.SettingCurrentyTypeFrom, SelectedFromCurrency.CurrencyType);
                SettingsService.AddValue(Constants.SettingCurrentyTypeTo, SelectedToCurrency.CurrencyType);
            }
        }

        internal void LoadSettings()
        {
            if (SelectedFromCurrency != null && SelectedToCurrency != null)
            {
                SelectedFromCurrency.CurrencyType = SettingsService.GetValue(Constants.SettingCurrentyTypeFrom);
                SelectedToCurrency.CurrencyType = SettingsService.GetValue(Constants.SettingCurrentyTypeTo);
            }
        }
    }
}