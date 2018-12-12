namespace Cashier.ViewModel
{
    public interface IProductTableViewModel
    {
        ProductsObservableCollection Products { get; set; }
        //ObservableCollection<ProductTableViewRow> Products { get; set; }
    }
}