using Cashier.Messages;
using Cashier.Services;

using Mvvm;

using System.Linq;
namespace Cashier.ViewModel
{
    public class ProductTableViewModel : ViewModelBase, IProductTableViewModel {

        private const string PRODUCTS_PROPRTY_NAME = "Products";
        //public ObservableCollection<ProductTableViewRow> Products { get; set; }
        public ProductsObservableCollection Products { get; set; }

        private ProductTableViewRow _currentProduct;
        private const string CURRENT_PRODUCT_PROPERTY_NAME = "CurrentProduct";

        public ProductTableViewRow CurrentProduct
        {
            get { return _currentProduct; }
            set {
                Set(CURRENT_PRODUCT_PROPERTY_NAME, ref _currentProduct, value);
                //TODO: Fix this
                Messenger.Default.Send(
                    new CurrentProductChangedMessage(value));
            }
        }

        private int _currentIndex;
        private const string CURRENT_INDEX_PROPERTY_NAME = "CurrentIndex";
        private readonly IDialogService _dialogService;
        private readonly IProductDataService _productDataService;

        public int CurrentIndex
        {
            get { return _currentIndex; }
            set { Set(CURRENT_INDEX_PROPERTY_NAME, ref _currentIndex, value); }
        }

        public ProductTableViewModel(IDialogService dialogService, 
                IProductDataService productDataService)
        {
            //Products = new ObservableCollection<ProductTableViewRow>();
            Products = new ProductsObservableCollection();
            _dialogService = dialogService;
            _productDataService = productDataService;
            Messenger.Default.Register<PaymentMessage>(
                                this, HandlePaymentMessage, 2);
        }

        private async void HandlePaymentMessage(PaymentMessage message)
        {
            if(message.PaymentType == PaymentType.Complete)
            {
                await _productDataService.UpdateSaldoByRange(Products.ToList());
                Products.Clear();
                Messenger.Default.Send(new CurrentProductChangedMessage(null));
                Messenger.Default.Send(new SumChangedMessage(0, 0, 0));
            }
        }

        private RelayCommand _changeQuantityCommand;

        public RelayCommand ChangeQuantityCommand
        {
            get
            {
                return _changeQuantityCommand
                    ?? (_changeQuantityCommand = new RelayCommand(
                    () =>
                    {
                    _dialogService.ChangeQuantity(CurrentProduct.Quantity,
                        (q) =>
                        {
                            var oldSum = CalculateSum();

                            var tmpIndex = CurrentIndex;
                            if (q > CurrentProduct.RemainderOfDebt)
                            {
                                _dialogService.ShowMessage(
                                    $"На остатке ({CurrentProduct.RemainderOfDebt}) " +
                                    $"не хватает  запрошенного количества ({q}) " +
                                    $"товара {CurrentProduct.Name}",
                                    "Ошибка изменения количества");
                                return;
                            }
                            CurrentProduct.Quantity = q;
                            Products.RaiseReplaceCollectionChanged();
                            CurrentIndex = tmpIndex;
                            Messenger.Default.Send(new CurrentProductChangedMessage(
                                    CurrentProduct));

                                var newSum = CalculateSum();

                                var delta = CurrentProduct.Price * CurrentProduct.Quantity;

                                var message = new SumChangedMessage()
                                {
                                    OldValue = oldSum,
                                    NewValue = newSum,
                                    Delta = delta
                                };
                            Messenger.Default.Send(message);
                            });
                    },
                    () => CurrentProduct != null));
            }
        }

        private RelayCommand _addProductCommand;

        public RelayCommand AddProductCommand
        {
            get
            {
                return _addProductCommand
                    ?? (_addProductCommand = new RelayCommand(
                    () =>
                    {
                        var oldSum = CalculateSum();
                        _dialogService.AddProduct((p) =>
                        {
                            Products.Add(p);
                            UpdateCurrentIndex();
                        });
                        var newSum = CalculateSum();
                        var delta = newSum - oldSum;
                                var message = new SumChangedMessage()
                                {
                                    OldValue = oldSum,
                                    NewValue = newSum,
                                    Delta = delta
                                };
                        Messenger.Default.Send(message);
                    },
                    () => true));
            }
        }
        private void UpdateCurrentIndex()
        {
            CurrentIndex = Products.Count - 1;
        }

        private double CalculateSum()
        {
            return Products.Sum((p) =>
            {
                return p.Price * p.Quantity;
            });
        }
    }
}
