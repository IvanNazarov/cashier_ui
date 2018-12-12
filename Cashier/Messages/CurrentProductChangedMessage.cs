using Cashier.ViewModel;

using System;

namespace Cashier.Messages
{
    public class CurrentProductChangedMessage
    {

        public CurrentProductChangedMessage(ProductTableViewRow item)
        {
            Product = item;
            EventDate = DateTime.Now;
        }

        public CurrentProductChangedMessage()
        {

        }

        public ProductTableViewRow Product { get; set; }
        public DateTime EventDate { get; set; }
    }
}
