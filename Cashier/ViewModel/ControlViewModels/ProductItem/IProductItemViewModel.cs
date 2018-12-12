namespace Cashier.ViewModel
{
    public interface IProductItemViewModel
    {
        int Id { get; set; }
        bool IsWeight { get; set; }
        string MeasureName { get; set; }
        string Name { get; set; }
        double Price { get; set; }
        double RemainderOfDebt { get; set; }
        double Quantity { get; set; }
   }
}