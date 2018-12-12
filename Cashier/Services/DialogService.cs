using Cashier.Dialogs;
using Cashier.ViewModel;
using Cashier.ViewModel.DialogViewModels;

using System;

namespace Cashier.Services
{
    public class DialogService : IDialogService
    {
        private readonly IProductDataService _productDataService;

        public void ShowMessage(string message, 
            string title = "", 
            Action callback = null)
        {
            var DialogVm = new MessageBoxViewModel(message, title, callback);
            var Dialog = new MessageBoxWindow
            {
                DataContext = DialogVm
            };
            DialogVm.CloseAction = new Action(Dialog.Close);
            Dialog.Show();
        }

        public void ChangeQuantity(double currentValue,
            Action<double> newValueCallback)
        {
            var vm = new ChangeQuantityViewModel(currentValue, newValueCallback);
            var view = new ChangeQuantityWindow
            {
                DataContext = vm
            };
            vm.CloseAction = new Action(view.Close);
            view.ShowDialog();
        }

        public void AddProduct(Action<ProductTableViewRow> callback)
        {
            var vm = new AddProductViewModel(_productDataService, callback);
            var view = new AddProductWindow
            {
                DataContext = vm
            };
            vm.CloseAction = new Action(view.Close);
            view.ShowDialog();
        }

        public void Payment(double documentSum, Action<double> callback)
        {
            var vm = new PaymentViewModel(documentSum, callback);
            var view = new PaymentWindow
            {
                DataContext = vm
            };
            vm.CloseAction = new Action(view.Close);
            view.ShowDialog();
        }

        public DialogService(IProductDataService productDataService)
        {
            _productDataService = productDataService;
        }
    }
}
