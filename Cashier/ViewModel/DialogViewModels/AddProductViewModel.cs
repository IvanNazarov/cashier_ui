using Cashier.Services;

using Mvvm;

using System;
using System.Collections.ObjectModel;

namespace Cashier.ViewModel.DialogViewModels
{
    public class AddProductViewModel : ViewModelBase
    {
        private readonly IProductDataService _productDataService;

        public ObservableCollection<ProductTableViewRow> Products { get; set; }

        public Action CloseAction { get; set; }
        public Action<ProductTableViewRow> CallbackAction { get; set; }

        public AddProductViewModel(IProductDataService productDataService,
            Action<ProductTableViewRow> callbackAction)
        {
            Products = new ObservableCollection<ProductTableViewRow>();
            Messenger.Default.Register<string>(this, FindProduct);
            _productDataService = productDataService;
            CallbackAction = callbackAction;
        }

        private async void FindProduct(string ss)
        {
            var products = await _productDataService.GetAsync((p) => 
                    p.Name.ToUpper().Contains(ss.ToUpper()));

            await App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                Products.Clear();
                foreach (var item in products)
                {
                    Products.Add(item);
                }
                if (Products.Count > 0)
                    SelectedItem = Products[0];
            }));
        }

        private string _searchString;
        private const string SEARCH_STRING_PROPERTY_NAME = "SearchString";

        public string SearchString
        {
            get { return _searchString; }
            set {
                Set(SEARCH_STRING_PROPERTY_NAME, ref _searchString, value);
                if(_searchString.Length > 2)
                    Messenger.Default.Send(value);
            }
        }

        private ProductTableViewRow _selectedItem;
        private const string SELECTED_ITEM_PROPERTY_NAME = "SelectedItem";

        public ProductTableViewRow SelectedItem
        {
            get { return _selectedItem; }
            set { Set(SELECTED_ITEM_PROPERTY_NAME, ref _selectedItem, value); }
        }

        private RelayCommand _closeCommand;

        public RelayCommand CloseCommand
        {
            get
            {
                return _closeCommand
                    ?? (_closeCommand = new RelayCommand(
                    () =>
                    {
                        CloseAction?.Invoke();
                    }));
            }
        }
        private RelayCommand _proceedCommand;

        public RelayCommand ProceedCommand
        {
            get
            {
                return _proceedCommand
                    ?? (_proceedCommand = new RelayCommand(
                    () =>
                    {
                        CallbackAction?.Invoke(SelectedItem);
                        CloseAction?.Invoke();
                    },
                    () => SelectedItem != null 
                    && SelectedItem.Quantity <= SelectedItem.RemainderOfDebt));
            }
        }
    }
}
