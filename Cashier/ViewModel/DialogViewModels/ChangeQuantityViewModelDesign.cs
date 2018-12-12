
using Mvvm;

namespace Cashier.ViewModel.DialogViewModels
{
    public class ChangeQuantityViewModelDesign : ViewModelBase
    {
        private string _quantity = "10000";
        private const string QUANTITY_PROPERTY_NAME = "Quantity";

        public string Quantity
        {
            get { return _quantity; }
            set { Set(QUANTITY_PROPERTY_NAME, ref _quantity, value); }
        }

        private RelayCommand<string> _addSymbolCommand;

        /// <summary>
        /// Gets the AddSymbolCommand.
        /// </summary>
        public RelayCommand<string> AddSymbolCommand
        {
            get
            {
                return _addSymbolCommand
                    ?? (_addSymbolCommand = new RelayCommand<string>(
                    symbol =>
                    {
                        Quantity += symbol;
                    }));
            }
        }}
}
