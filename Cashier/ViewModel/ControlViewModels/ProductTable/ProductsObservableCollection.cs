using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Cashier.ViewModel
{
    public class ProductsObservableCollection : 
                                ObservableCollection<ProductTableViewRow>
    {
        public void RaiseReplaceCollectionChanged()
        {
            OnCollectionChanged(
                new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Reset));
        }
    }
}
