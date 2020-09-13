using CurrencyConverter.UI.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverter.UI.ViewModels
{
    public interface ITestViewModel
    {
        public void Load(int id);
    }
    public class TestViewModel : Observable, ITestViewModel
    {
        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        public TestViewModel(SimpleNavigationService nav)
        {
            Name = "JOrge";
        }

        public void Load(int id)
        {
            Name = "JOrge3";
        }
    }
}
