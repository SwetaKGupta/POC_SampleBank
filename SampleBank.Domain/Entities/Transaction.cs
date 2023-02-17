namespace SampleBank.Domain.Entities
{
    /// <summary>
    /// Model for TransactionInfo
    /// </summary>
    public class Transaction
    {
        public Guid Id { get; set; }
        public string TransactionReference { get; set; }
        public Guid SourceAccountId { get; set; }
        public decimal Amount { get; set; }
        public Guid DestinationAccountId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
