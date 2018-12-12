using Cashier.Messages;

using Mvvm;

namespace Cashier.ViewModel
{
    public class DocumentInfoViewModelDesign : ViewModelBase, IDocumentInfoViewModel
    {
        public DocumentInfoViewModelDesign()
        {
            Messenger.Default.Register<SumChangedMessage>(this, (sc) =>
            {
                DocumentSum = sc.NewValue;
            });
        }

        private int _documentId = 7;
        private const string DOCUMENT_ID_PROPERTY_NAME = "DocumentId";

        public int DocumentId
        {
            get { return _documentId; }
            set { Set(DOCUMENT_ID_PROPERTY_NAME, ref _documentId, value); }
        }


        private double _documentSum = 10000000;
        private const string DOCUMENT_SUM_PROPERTY_NAME = "DocumentSum";

        public double DocumentSum
        {
            get { return _documentSum; }
            set { Set(DOCUMENT_SUM_PROPERTY_NAME, ref _documentSum, value); }
        }


        private double _discountSum;
        private const string DISCOUNT_SUM_PROPERTY_NAME = "DiscountSum";

        public double DiscountSum
        {
            get { return _discountSum; }
            set { Set(DISCOUNT_SUM_PROPERTY_NAME, ref _discountSum, value); }
        }

        private RelayCommand _payCashCommand;

        /// <summary>
        /// Gets the PayCashCommand.
        /// </summary>
        public RelayCommand PayCashCommand
        {
            get
            {
                return _payCashCommand
                    ?? (_payCashCommand = new RelayCommand(
                    () =>
                    {
                        System.Windows.MessageBox.Show("Тут будет диалог оплаты");
                    },
                    () => _documentSum > 0));
            }
        }

        private RelayCommand _payCardCommand;

        /// <summary>
        /// Gets the PayCardCommand.
        /// </summary>
        public RelayCommand PayCardCommand
        {
            get
            {
                return _payCardCommand
                    ?? (_payCardCommand = new RelayCommand(
                    () =>
                    {
                        System.Windows.MessageBox.Show("Здеся платим картой");
                    },
                    () => _documentSum > 0));
            }
        }
    }
}
