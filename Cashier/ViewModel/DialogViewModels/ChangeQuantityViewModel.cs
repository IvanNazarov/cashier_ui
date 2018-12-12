
using Mvvm;

using System;
using System.Globalization;

namespace Cashier.ViewModel.DialogViewModels
{
    public class ChangeQuantityViewModel : ViewModelBase
    {
        public Action CloseAction { get; set; }

        public Action<double> NewValueCallback { get; set; }

        private string _quantity;
        private const string QUANTITY_PROPERTY_NAME = "Quantity";

        public string Quantity
        {
            get { return _quantity; }
            set { Set(QUANTITY_PROPERTY_NAME, ref _quantity, value); }
        }

        private bool _firstTime;

        private RelayCommand<string> _addSymbolCommand;
        public RelayCommand<string> AddSymbolCommand
        {
            get
            {
                return _addSymbolCommand
                    ?? (_addSymbolCommand = new RelayCommand<string>(
                    symbol =>
                    {
                        if (_firstTime)
                        {
                            if(symbol != "B")
                                Quantity = "";
                            _firstTime = false;
                        }

                        if (symbol == "C")
                        {
                            if (Quantity == "")
                                CloseAction?.Invoke();
                            Quantity = "";
                            return;
                        }
                        if (symbol == "B" && Quantity != "")
                        {
                            Quantity = _quantity.Substring(0, _quantity.Length - 1);
                            return;
                        }
                        if ((symbol == "B" || symbol == ".") && Quantity == "")
                            return;

                        if(symbol == ".")
                        {
                            if (Quantity.Contains(symbol))
                                return;
                        }
                        Quantity += symbol;
                    }));
            }
        }

        private RelayCommand _okCommand;
        public RelayCommand OkCommand
        {
            get
            {
                return _okCommand
                    ?? (_okCommand = new RelayCommand(
                    () =>
                    {
                        NewValueCallback?.Invoke(double.Parse(_quantity,
                            CultureInfo.InvariantCulture));
                        CloseAction?.Invoke();
                    },
                    () => {
                        return !string.IsNullOrEmpty(_quantity) && 
                                        double.Parse(_quantity,
                                        CultureInfo.InvariantCulture) > 0;
                    }));
            }
        }

        public ChangeQuantityViewModel()
        {
            _firstTime = true;
        }

        public ChangeQuantityViewModel(double quantity, Action<double> callback):
            this()
        {
            Quantity = quantity.ToString("0.00",CultureInfo.InvariantCulture);
            NewValueCallback = callback;
        }
        
    }
}
