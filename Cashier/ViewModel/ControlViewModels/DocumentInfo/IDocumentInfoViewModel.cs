using Mvvm;

namespace Cashier.ViewModel
{
    public interface IDocumentInfoViewModel
    {
        double DiscountSum { get; set; }
        int DocumentId { get; set; }
        double DocumentSum { get; set; }
        RelayCommand PayCardCommand { get; }
        RelayCommand PayCashCommand { get; }
    }
}