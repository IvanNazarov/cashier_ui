namespace Cashier.Messages
{
    public class SumChangedMessage
    {
        public double OldValue { get; set; }
        public double NewValue { get; set; }
        public double Delta { get; set; }

        public SumChangedMessage()
        {

        }
        public SumChangedMessage(double oldValue, 
            double newValue, double delta) : this()
        {
            OldValue = oldValue;
            NewValue = newValue;
            Delta = delta;
        }
    }


}
