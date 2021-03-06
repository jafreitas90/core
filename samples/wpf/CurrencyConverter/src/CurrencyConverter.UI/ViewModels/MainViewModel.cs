﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AsyncAwaitBestPractices.MVVM;
using CurrencyConverter.Model;
using CurrencyConverter.Services.Synchronizers;
using CurrencyConverter.UI.DataProvider;
using CurrencyConverter.UI.DataProvider.Lookups;
using CurrencyConverter.UI.Model.Enums;
using CurrencyConverter.UI.Utilities;
using CurrencyConverter.UI.Utilities.Settings;
using CurrencyConverter.UI.Wrapper;

namespace CurrencyConverter.UI.ViewModels
{
    public class MainViewModel : Observable
    {
        #region Fields
        private bool _isBusy;
        private bool _isDataSynchronized;
        private ISynchronizer _synchronizer;
        private ILookupProvider<Rate> _currencyTypeLookupProvider;
        private ExchangeRatesWrappper _exchangeRates;
        private CurrencyWrapper _selectedFromCurrency;
        private CurrencyWrapper _selectedToCurrency;
        private IEnumerable<string> _currencyTypeGroupLookup;
        private IExchangeRateDataProvider _exchangeRateDataProvider;
        private float _from;
        #endregion

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

        #region Properties
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

        public IEnumerable<string> CurrencyTypeGroupLookup
        {
            get { return _currencyTypeGroupLookup; }
            set
            {
                _currencyTypeGroupLookup = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructor
        public MainViewModel(ISynchronizer synchronizer, ILookupProvider<Rate> currencyTypeLookupProvider, IExchangeRateDataProvider exchangeRateDataProvider)
        {
            _exchangeRateDataProvider = exchangeRateDataProvider;
            _currencyTypeLookupProvider = currencyTypeLookupProvider;
            _synchronizer = synchronizer;
            SyncCommand = new AsyncCommand(canExecute: _ => !IsBusy, execute: OnSyncDataExecute, onException: OnSyncDataException);

            // load data
            Task.Run(async () => await LoadAsync()).GetAwaiter().GetResult();
        }
        #endregion

        #region Private Methods
        private void ExchangeFromToCurrency(CurrencyChangePropertyEnum changePropertyEnum)
        {
            CalculeExchangeRate(SelectedFromCurrency, SelectedToCurrency);
        }

        private void ExchangeToFromCurrency(CurrencyChangePropertyEnum changePropertyEnum)
        {
            var from = SelectedToCurrency;
            var to = SelectedFromCurrency;
            if (changePropertyEnum == CurrencyChangePropertyEnum.CurrencyType)
            {
                from = SelectedFromCurrency;
                to = SelectedToCurrency;
            }
            CalculeExchangeRate(from, to);
        }

        private void CalculeExchangeRate(CurrencyWrapper from, CurrencyWrapper to)
        {
            var fromCurrencyType = from.CurrencyType;
            var toCurrencyType = to.CurrencyType;
            var amount = from.Value;

            UnSubscribeCalculeExhangeCurrencyEvents();
            to.Value = ExchangeRates.Model.GetExchangeRate(fromCurrencyType, toCurrencyType, amount);
            SubscribeCalculeExhangeCurrencyEvents();
        }

        private async Task LoadAsync()
        {
            UnSubscribeCalculeExhangeCurrencyEvents();
            // Get currencies types to lookup
            CurrencyTypeGroupLookup = await _currencyTypeLookupProvider.GetLookupAsync();

            // if no data (first time loading)
            if (CurrencyTypeGroupLookup == null)
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
                    SelectedFromCurrency = new CurrencyWrapper(new Rate(ExchangeRates.Base.CurrencyType, 1));

                    LoadSettings();
                }
                SubscribeCalculeExhangeCurrencyEvents();
                CalculeExchangeRate(SelectedFromCurrency, SelectedToCurrency);
            }
        }

        private void UnSubscribeCalculeExhangeCurrencyEvents()
        {
            if(SelectedToCurrency != null)
            {
                SelectedToCurrency.CurrencyChangedEvent -= ExchangeToFromCurrency;
            }

            if (SelectedFromCurrency != null)
            {
                SelectedFromCurrency.CurrencyChangedEvent -= ExchangeFromToCurrency;
            }
        }

        private void SubscribeCalculeExhangeCurrencyEvents()
        {
            if (SelectedToCurrency != null)
            {
                SelectedToCurrency.CurrencyChangedEvent += ExchangeToFromCurrency;
            }

            if (SelectedFromCurrency != null)
            {
                SelectedFromCurrency.CurrencyChangedEvent += ExchangeFromToCurrency;
            }
        }

        private async Task OnSyncDataExecute()
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

        private void OnSyncDataException(Exception ex)
        {
            // show some message, write to logs..
            MessageBox.Show("Something went wrong. Contact the system administration please.");
        }
        #endregion

        #region Internal
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
                SelectedToCurrency.CurrencyType = SettingsService.GetValue(Constants.SettingCurrentyTypeTo);
                SelectedFromCurrency.CurrencyType = SettingsService.GetValue(Constants.SettingCurrentyTypeFrom);
            }
        }
        #endregion
    }
}