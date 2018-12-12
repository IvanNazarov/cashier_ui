using Mvvm;

using System;
using System.Globalization;

namespace Cashier.ViewModel.DialogViewModels
{
    public class PaymentViewModel : ViewModelBase
    {
        #region Properties

        #region Payment
        private string _paymentText;
        private const string PAYMENT_TEXT_PROPERTY_NAME = "PaymentText";

        public string PaymentText
        {
            get { return _paymentText; }
            set
            {
                Set(PAYMENT_TEXT_PROPERTY_NAME, ref _paymentText, value);
                RaisePropertyChanged(SHORT_CHANGE_PROPERTY_NAME);
            }
        }
        #endregion

        #region DocumentSum
        private double _documentSum; 
        private const string DOCUMENT_SUM_PROPERTY_NAME = "DocumentSum";

        public double DocumentSum
        {
            get { return _documentSum; }
            set { Set(DOCUMENT_SUM_PROPERTY_NAME, ref _documentSum, value); }
        }
        #endregion

        #region ShortChange
        private const string SHORT_CHANGE_PROPERTY_NAME = "ShortChange";
        public double ShortChange
        {
            get
            {
                return double.TryParse(_paymentText, out double val)
                                        ? val - _documentSum : 0.0;
            }
        }
        #endregion

        public Action CloseAction { get; set; }

        public Action<double> NewValueCallback { get; set; }
        #endregion

        #region Commands
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
                            if (symbol != "B")
                                PaymentText = "";
                            _firstTime = false;
                        }

                        if (symbol == "C")
                        {
                            if (PaymentText == "")
                                CloseAction?.Invoke();
                            PaymentText = "";
                            return;
                        }
                        if (symbol == "B" && PaymentText != "")
                        {
                            PaymentText = _paymentText.Substring(0, _paymentText.Length - 1);
                            return;
                        }
                        if ((symbol == "B" || symbol == ".") && PaymentText == "")
                            return;

                        if (symbol == ".")
                        {
                            if (PaymentText.Contains(symbol))
                                return;
                        }
                        PaymentText += symbol;
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
                        NewValueCallback?.Invoke(double.Parse(_paymentText,
                            CultureInfo.InvariantCulture));
                        CloseAction?.Invoke();
                    },
                    () =>
                    {
                        return !string.IsNullOrEmpty(_paymentText) &&
                                        double.Parse(_paymentText,
                                        CultureInfo.InvariantCulture) > 0;
                    }));
            }
        }
        #endregion

        #region Constructors
        public PaymentViewModel()
        {
            _firstTime = true;
        }

        public PaymentViewModel(double sum, Action<double> callback) :
            this()
        {
            DocumentSum = sum;
            PaymentText = sum.ToString("0.00", CultureInfo.InvariantCulture);
            NewValueCallback = callback;
        }
        #endregion
    }
}
