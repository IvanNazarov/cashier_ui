using Cashier.Messages;
using Cashier.Services;

using Mvvm;

namespace Cashier.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private RelayCommand<int> _payCommand;
        private readonly IDialogService _ds;

        public RelayCommand<int> PayCommand
        {
            get
            {
                return _payCommand
                    ?? (_payCommand = new RelayCommand<int>(
                    (p) =>
                    {
                        Messenger.Default.Send( new PaymentMessage((PaymentType)p),1);
                    }));
            }
        }

        public MainViewModel(IDialogService ds)
        {
            _ds = ds;
        }
    }
}
