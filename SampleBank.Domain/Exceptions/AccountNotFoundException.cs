using SampleBank.Domain.Exceptions.Base;

namespace SampleBank.Domain.Exceptions
{
    /// <summary>
    /// Exception is thrown for Account Not Found
    /// </summary>
    public sealed class AccountNotFoundException : NotFoundException
    {
        public AccountNotFoundException(string accountId)
            : base($"The account with the Id {accountId} was not found.")
        {
        }
    }
}