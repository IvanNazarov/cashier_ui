
using Mvvm;

using System;

namespace Cashier.ViewModel.DialogViewModels
{
    public class MessageBoxViewModel : ViewModelBase
    {

        private string _title;
        private const string TITLE_PROPERTY_NAME = "Title";

        public string Title
        {
            get { return _title; }
            set { Set(TITLE_PROPERTY_NAME, ref _title, value); }
        }


        private string _message;
        private const string MESSAGE_PROPERTY_NAME = "Message";

        public string Message
        {
            get { return _message; }
            set { Set(MESSAGE_PROPERTY_NAME, ref _message, value); }
        }
        public Action CloseAction { get; set; }

        public Action CallbackAction { get; set; }

        public MessageBoxViewModel()
        {

        }

        public MessageBoxViewModel( string message, string title = "",
                                    Action callback = null)
        {
            _message = message;
            _title = title;
            CallbackAction = callback;
        }

        private RelayCommand _okCommand;

        public RelayCommand OkCommand
        {
            get
            {
                return _okCommand
                    ?? (_okCommand = new RelayCommand(
                    () =>
                    {
                        if (CallbackAction != null)
                            CallbackAction.Invoke();

                        if (CloseAction != null)
                            CloseAction.Invoke();
                    }));
            }
        }
    }
}
