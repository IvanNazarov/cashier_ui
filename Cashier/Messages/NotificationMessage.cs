namespace Cashier.Messages
{
    public class NotificationMessage
    {
        public string Notification { get; set; }

        public NotificationMessage()
        {

        }

        public NotificationMessage(string notification) : this()
        {
            Notification = notification;
        }
    }
}
