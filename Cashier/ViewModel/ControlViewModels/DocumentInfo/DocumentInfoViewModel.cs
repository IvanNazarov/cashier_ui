using Cashier.Messages;
using Cashier.Services;

using Mvvm;

namespace Cashier.ViewModel
{
    public class DocumentInfoViewModel : ViewModelBase, IDocumentInfoViewModel
    {
        private readonly IDialogService _dialogService;

        #region Constructors
        public DocumentInfoViewModel(IDialogService dialogService)
        {
            Messenger.Default.Register<SumChangedMessage>(this, (sc) =>
            {
                DocumentSum = sc.NewValue;
            });
            Messenger.Default.Register<PaymentMessage>(this, HandlePaymentMessage,1);
            _dialogService = dialogService;
        }

        private void HandlePaymentMessage(PaymentMessage message)
        {
            if(message.PaymentType == PaymentType.Cash)
            {
                if (PayCashCommand.CanExecute(null))
                    PayCashCommand.Execute(null);
            }
            else if (message.PaymentType == PaymentType.Card)
            {
                if (PayCardCommand.CanExecute(null))
                    PayCardCommand.Execute(null);
            }
        }
        #endregion

        #region DoumentId
        private int _documentId;
        private const string DOCUMENT_ID_PROPERTY_NAME = "DocumentId";

        public int DocumentId
        {
            get { return _documentId; }
            set { Set(DOCUMENT_ID_PROPERTY_NAME, ref _documentId, value); }
        }
        #endregion

        #region DoumentSum
        private double _documentSum;
        private const string DOCUMENT_SUM_PROPERTY_NAME = "DocumentSum";

        public double DocumentSum
        {
            get { return _documentSum; }
            set { Set(DOCUMENT_SUM_PROPERTY_NAME, ref _documentSum, value); }
        }
        #endregion

        #region DiscountSum
        private double _discountSum;
        private const string DISCOUNT_SUM_PROPERTY_NAME = "DiscountSum";

        public double DiscountSum
        {
            get { return _discountSum; }
            set { Set(DISCOUNT_SUM_PROPERTY_NAME, ref _discountSum, value); }
        }
        #endregion

        #region Commands
        private RelayCommand _payCashCommand;

        public RelayCommand PayCashCommand
        {
            get
            {
                return _payCashCommand
                    ?? (_payCashCommand = new RelayCommand(
                    () =>
                    {
                        _dialogService.Payment(DocumentSum, (p) =>
                        {
                            _dialogService.ShowMessage($"Наличными {p}",
                                "Пришла оплата");
                            Messenger.Default.Send(
                                new PaymentMessage(PaymentType.Complete),2);
                        });
                    },
                    () => _documentSum > 0));
            }
        }

        private RelayCommand _payCardCommand;

        public RelayCommand PayCardCommand
        {
            get
            {
                return _payCardCommand
                    ?? (_payCardCommand = new RelayCommand(
                    () =>
                    {
                        _dialogService.ShowMessage("Картой", "Оплата");
                    },
                    () => _documentSum > 0));
            }
        }
        #endregion
    }
}
