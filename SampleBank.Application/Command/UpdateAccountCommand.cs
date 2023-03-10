using MediatR;
using SampleBank.Domain.Entities;

namespace SampleBank.Application.Command
{
    /// <summary>
    /// Record for Update Account Command
    /// </summary>
    /// <param name="account"></param>
    public record UpdateAccountCommand(Account account) : IRequest;
}
