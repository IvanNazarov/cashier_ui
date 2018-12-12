namespace Cashier.Messages
{
    public class PaymentMessage
    {
        public PaymentType PaymentType { get; set; }

        public PaymentMessage()
        {

        }
        public PaymentMessage(PaymentType paymentType) : this()
        {
            PaymentType = paymentType;
        }
    }
}
