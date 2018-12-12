using Mvvm;

namespace Cashier.ViewModel
{
    public class ProductitemViewModel : ViewModelBase, IProductItemViewModel
    {
        public int Id { get; set; } = 1;

        private string _name = "Кеторол (10мг)";
        private const string NAME_PROPERTY_NAME = "Name";

        public string Name
        {
            get { return _name; }
            set { Set(NAME_PROPERTY_NAME, ref _name, value); }
        }


        private string _measureName = "упак.";
        private const string MEASURE_NAME_PROPERTY_NAME = "MeasureName";

        public string MeasureName
        {
            get { return _measureName; }
            set { Set(MEASURE_NAME_PROPERTY_NAME, ref _measureName, value); }
        }

        private double _quantity = 10.000;
        private const string QUANTITY_PROPERTY_NAME = "Quantity";

        public double Quantity
        {
            get { return _quantity; }
            set { Set(QUANTITY_PROPERTY_NAME, ref _quantity, value); }
        }

        private double _price = 100.00;
        private const string PRICE_PROPERTY_NAME = "Price";

        public double Price
        {
            get { return _price; }
            set { Set(PRICE_PROPERTY_NAME, ref _price, value); }
        }


        private bool _isWeight = false;
        private const string IS_WEIGHT_PROPERTY_NAME = "IsWeight";

        public bool IsWeight
        {
            get { return _isWeight; }
            set { Set(IS_WEIGHT_PROPERTY_NAME, ref _isWeight, value); }
        }

        private double _sum = 1000.00;
        private const string SUM_PROPERTY_NAME = "Sum";

        public double Sum
        {
            get { return _sum; }
            set { Set(SUM_PROPERTY_NAME, ref _sum, value); }
        }
       private double _remainderOfDebt = 25;
        private const string REMAINDER_OF_DEBT_PROPERTY_NAME = "RemainderOfDebt";

        public double RemainderOfDebt
        {
            get { return _remainderOfDebt; }
            set { Set(REMAINDER_OF_DEBT_PROPERTY_NAME, ref _remainderOfDebt, value); }
        }
    }
}
