using Cashier.Messages;
using Cashier.Services;

using Mvvm;

using System;
using System.Linq;

namespace Cashier.ViewModel
{
    public class ProductTableViewModelDesign : ViewModelBase,
                                               IProductTableViewModel
    {
        //public ObservableCollection<ProductTableViewRow> Products { get; set; }
        public ProductsObservableCollection Products { get; set; }

        private ProductTableViewRow _currentProduct;
        private const string CURRENT_PRODUCT_PROPERTY_NAME = "CurrentProduct";

        public ProductTableViewRow CurrentProduct
        {
            get { return _currentProduct; }
            set { Set(CURRENT_PRODUCT_PROPERTY_NAME, ref _currentProduct, value); }
        }


        private int _currentIndex;
        private const string CURRENT_INDEX_PROPERTY_NAME = "CurrentIndex";
        private readonly IDialogService _dialogService;

        public int CurrentIndex
        {
            get { return _currentIndex; }
            set { Set(CURRENT_INDEX_PROPERTY_NAME, ref _currentIndex, value); }
        }

        public ProductTableViewModelDesign(IDialogService dialogService)
        {
            Products = new ProductsObservableCollection()
            //Products = new ObservableCollection<ProductTableViewRow>()
            {
                new ProductTableViewRow
                {
                    Id = 1,
                    Name = "Кеторол",
                    MeasureName = "шт.",
                    Price = 100,
                    Quantity = 3,
                    RemainderOfDebt = 25,
                    IsWeight = false,
                    MeasureId = 1
                }
            };
            UpdateCurrentIndex();
            _dialogService = dialogService;
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

                                CurrentProduct.Quantity = q;

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
                        ProductTableViewRow pivm = new ProductTableViewRow()
                        {
                            Name = "Аспирин",
                            MeasureName = "шт.",
                            Price = 127.60,
                            Quantity = 8,
                            IsWeight = false
                        };
                        var newSum = CalculateSum();
                        var delta = pivm.Quantity * pivm.Price;
                                var message = new SumChangedMessage()
                                {
                                    OldValue = oldSum,
                                    NewValue = newSum,
                                    Delta = delta
                                };
                        Messenger.Default.Send(message);
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            Products.Add(pivm);
                            UpdateCurrentIndex();
                        }));
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
            return Products.Sum<ProductTableViewRow>((p) =>
            {
                return p.Price * p.Quantity;
            });
        }
    }
}
