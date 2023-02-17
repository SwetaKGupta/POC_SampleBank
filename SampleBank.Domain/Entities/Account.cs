namespace SampleBank.Domain.Entities
{
    /// <summary>
    /// Model for Account
    /// </summary>
    public class Account
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string AccountNumber { get; set; }
        public decimal AccountBalance { get; set; }
    }
}
