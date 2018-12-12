using Cashier.ViewModel;

using System;

namespace Cashier.Services
{
    public interface IDialogService
    {
        void AddProduct(Action<ProductTableViewRow> callback);
        void ChangeQuantity(double currentValue, Action<double> newValueCallback);
        void Payment(double documentSum, Action<double> callback);
        void ShowMessage(string message, string title = "", Action callback = null);
    }
}