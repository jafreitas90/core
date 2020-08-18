using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            //_originalValues = new Dictionary<string, object>();
            //_trackingObjects = new List<IValidatableTrackingObject>();
            //InitializeComplexProperties(model);
            InitializeCollectionProperties(model);
            //Validate();
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
                //UpdateOriginalValue(currentValue, newValue, propertyName);
                propertyInfo.SetValue(Model, newValue);
                //Validate();
                OnPropertyChanged(propertyName);
                //OnPropertyChanged(propertyName + "IsChanged");
            }
        }

        protected void RegisterCollection<TWrapper, TModel>(
         ObservableCollection<TWrapper> wrapperCollection,
         List<TModel> modelCollection) where TWrapper : ModelWrapper<TModel>
        {
            wrapperCollection.CollectionChanged += (s, e) =>
            {
                modelCollection.Clear();
                modelCollection.AddRange(wrapperCollection.Select(w => w.Model));
                //Validate();
            };
          //  RegisterTrackingObject(wrapperCollection);
        }
    }
}