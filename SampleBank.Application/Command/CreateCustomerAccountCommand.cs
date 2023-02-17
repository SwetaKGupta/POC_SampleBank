using MediatR;
using SampleBank.Domain.Entities;

namespace SampleBank.Application.Command
{
    /// <summary>
    /// Record for Customer Account Command
    /// </summary>
    /// <param name="account"></param>
    public record CreateCustomerAccountCommand(Account account) : IRequest;
}
