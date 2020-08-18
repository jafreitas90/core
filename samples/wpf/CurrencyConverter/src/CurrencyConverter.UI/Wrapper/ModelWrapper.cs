using System;
using System.Runtime.CompilerServices;
using CurrencyConverter.UI.ViewModels;

namespace CurrencyConverter.UI.Wrapper
{
    public class ModelWrapper<T> : Observable
    {
        public T Model { get; private set; }

        public ModelWrapper(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            Model = model;
            InitializeCollectionProperties(model);
        }

        protected virtual void InitializeCollectionProperties(T model)
        {
        }

        protected TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            var propertyInfo = Model.GetType().GetProperty(propertyName);
            return (TValue)propertyInfo.GetValue(Model);
        }

        protected void SetValue<TValue>(TValue newValue, [CallerMemberName] string propertyName = null)
        {
            var propertyInfo = Model.GetType().GetProperty(propertyName);
            var currentValue = propertyInfo.GetValue(Model);
            if (!Equals(currentValue, newValue))
            {
                propertyInfo.SetValue(Model, newValue);
                OnPropertyChanged(propertyName);
            }
        }
    }
}