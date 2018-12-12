using Cashier.Messages;

using Mvvm;

namespace Cashier.ViewModel
{
    public class ProductItemViewModel : ViewModelBase, IProductItemViewModel
    {
        #region Constructors
        public ProductItemViewModel()
        {
            Messenger.Default.Register<CurrentProductChangedMessage>(
                                    this, ChangeCurrentProduct);
        }

        //Copy constructor
        public ProductItemViewModel(ProductItemViewModel model) : this()
        {
            Id = model.Id;
            Name = model.Name;
            MeasureName = model.MeasureName;
            Quantity = model.Quantity;
            Price = model.Price;
            IsWeight = model.IsWeight;
        }
        #endregion

        #region Properties
        public int Id { get; set; }

        #region Name
        private string _name;
        private const string NAME_PROPERTY_NAME = "Name";

        public string Name
        {
            get { return _name; }
            set { Set(NAME_PROPERTY_NAME, ref _name, value); }
        }
        #endregion

        #region MeasureName
        private string _measureName;
        private const string MEASURE_NAME_PROPERTY_NAME = "MeasureName";

        public string MeasureName
        {
            get { return _measureName; }
            set { Set(MEASURE_NAME_PROPERTY_NAME, ref _measureName, value); }
        }
        #endregion

        #region Quantity
        private double _quantity;
        private const string QUANTITY_PROPERTY_NAME = "Quantity";

        public double Quantity
        {
            get { return _quantity; }
            set
            {
                Set(QUANTITY_PROPERTY_NAME, ref _quantity, value);
                RaisePropertyChanged(SUM_PROPERTY_NAME);
            }
        }
        #endregion

        #region Price
        private double _price;
        private const string PRICE_PROPERTY_NAME = "Price";

        public double Price
        {
            get { return _price; }
            set
            {
                Set(PRICE_PROPERTY_NAME, ref _price, value);
                RaisePropertyChanged(SUM_PROPERTY_NAME);
            }
        }
        #endregion

        #region IsWeight
        private bool _isWeight;
        private const string IS_WEIGHT_PROPERTY_NAME = "IsWeight";

        public bool IsWeight
        {
            get { return _isWeight; }
            set { Set(IS_WEIGHT_PROPERTY_NAME, ref _isWeight, value); }
        }
        #endregion

        #region Sum
        private const string SUM_PROPERTY_NAME = "Sum";
        public double Sum
        {
            get { return Quantity * Price; }
        }
        #endregion

        #region RemainderOfDebt
        private double _remainderOfDebt;
        private const string REMAINDER_OF_DEBT_PROPERTY_NAME = "RemainderOfDebt";

        public double RemainderOfDebt
        {
            get { return _remainderOfDebt; }
            set { Set(REMAINDER_OF_DEBT_PROPERTY_NAME, ref _remainderOfDebt, value); }
        }
        #endregion
        #endregion

        #region MessageHandlers
        private void ChangeCurrentProduct(CurrentProductChangedMessage message)
        {
            var model = message.Product;

            if (model == null)
            {
                Id = 0;
                Name = "";
                MeasureName = "";
                Quantity = 0;
                RemainderOfDebt = 0;
                Price = 0;
                IsWeight = false;
                return;
            }

            Id = model.Id;
            Name = model.Name;
            MeasureName = model.MeasureName;
            Quantity = model.Quantity;
            Price = model.Price;
            RemainderOfDebt = model.RemainderOfDebt;
            IsWeight = model.IsWeight;
        }
        #endregion
    }
}
