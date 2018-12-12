namespace Cashier.ViewModel
{
    public class ProductTableViewRow 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsWeight { get; set; }
        public double Quantity { get; set; }
        public double RemainderOfDebt { get; set; }
        public string MeasureName { get; set; }
        public int MeasureId { get; set; }
    }
}
