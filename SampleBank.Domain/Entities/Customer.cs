using System.ComponentModel.DataAnnotations;

namespace SampleBank.Domain.Entities
{
    /// <summary>
    /// Model for Customer
    /// </summary>
    public class Customer
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string CustomerId { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime MemberSince { get; set; }
    }
}
