namespace SampleBank.Domain.Exceptions
{
    /// <summary>
    /// Exception when payment is failed
    /// </summary>
    public sealed class PaymentFailedException : Exception
    {
        public PaymentFailedException() : base("Payment failed during the transfer")
        {
        }
    }
}
