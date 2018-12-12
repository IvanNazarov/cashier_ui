
using Mvvm;

using System;
using System.Collections.ObjectModel;

namespace Cashier.ViewModel.DialogViewModels
{
    public class AddProductViewModelDesign : ViewModelBase
    {
        public ObservableCollection<ProductTableViewRow> Products { get; set; }

        public Action CloseAction { get; set; }
        public Action<IProductItemViewModel> CallbackAction { get; set; }

        private string _searchString = "Средство от ящура \"Топорин\"";
        private const string SEARCH_STRING_PROPERTY_NAME = "SearchString";

        public string SearchString
        {
            get { return _searchString; }
            set { Set(SEARCH_STRING_PROPERTY_NAME, ref _searchString, value); }
        }


        private IProductItemViewModel _selectedItem;
        private const string SELECTED_ITEM_PROPERTY_NAME = "SelectedItem";

        public IProductItemViewModel SelectedItem
        {
            get { return _selectedItem; }
            set { Set(SELECTED_ITEM_PROPERTY_NAME, ref _selectedItem, value); }
        }

        public RelayCommand CloseCommand { get; set; }
        public RelayCommand ProceedCommand { get; set; }

        public AddProductViewModelDesign()
        {
            Products = new ObservableCollection<ProductTableViewRow>()
            {
                new ProductTableViewRow()
                {
                    Id = 1,
                    IsWeight = false,
                    MeasureName = "упак.",
                    Name = "Анальгин",
                    Price = 1000,
                    RemainderOfDebt = 200
                },
                new ProductTableViewRow()
                {
                    Id = 1,
                    IsWeight = false,
                    MeasureName = "упак.",
                    Name = "Аспирин",
                    Price = 5000,
                    RemainderOfDebt = 2
                },
                new ProductTableViewRow()
                {
                    Id = 1,
                    IsWeight = false,
                    MeasureName = "упак.",
                    Name = "Ухо-горло-бдыщь",
                    Price = 253.85,
                    RemainderOfDebt = 14
                },
                new ProductTableViewRow()
                {
                    Id = 1,
                    IsWeight = false,
                    MeasureName = "кг.",
                    Name = "Чернокнижный сбор №18",
                    Price = 720,
                    RemainderOfDebt = 58.765
                }
            };
        }

    }
}
