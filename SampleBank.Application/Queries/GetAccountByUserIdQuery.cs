using MediatR;
using SampleBank.Domain.Entities;

namespace SampleBank.Application.Queries
{
    /// <summary>
    /// Record for GetAccountByUserId
    /// </summary>
    /// <param name="userId"></param>
    public record GetAccountByUserIdQuery(Guid userId) : IRequest<Account>;
}
