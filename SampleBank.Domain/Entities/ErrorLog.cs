namespace SampleBank.Domain.Entities
{
    /// <summary>
    /// Model for Error
    /// </summary>
    public class ErrorLog
    {
        public Guid Id { get; set; }
        public string ErrorDetail { get; set; }
        public DateTime Created { get; set; }
    }
}
