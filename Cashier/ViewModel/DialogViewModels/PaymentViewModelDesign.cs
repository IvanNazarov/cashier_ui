
using Mvvm;

namespace Cashier.ViewModel.DialogViewModels
{
    public class PaymentViewModelDesign : ViewModelBase
    {
        private string _paymentText = "1000.50";
        private const string QUANTITY_PROPERTY_NAME = "Payment";

        public string PaymentText
        {
            get { return _paymentText; }
            set {
                Set(QUANTITY_PROPERTY_NAME, ref _paymentText, value);
                RaisePropertyChanged(SHORT_CHANGE_PROPERTY_NAME);
            }
        }

        private double _documentSum = 500.50;
        private const string DOCUMENT_SUM_PROPERTY_NAME = "DocumentSum";

        public double DocumentSum
        {
            get { return _documentSum; }
            set { Set(DOCUMENT_SUM_PROPERTY_NAME, ref _documentSum, value); }
        }

        private const string SHORT_CHANGE_PROPERTY_NAME = "ShortChange";
        public double ShortChange
        {
            get
            {
                return double.TryParse(_paymentText,out double val) 
                                        ? val - _documentSum : 0.0;
            }
        }
    }
}
