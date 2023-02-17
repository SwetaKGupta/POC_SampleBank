using MediatR;
using SampleBank.Domain.Entities;

namespace SampleBank.Application.Queries
{
    /// <summary>
    /// Record for GetAllTransactionsOfUser
    /// </summary>
    /// <param name="accountId"></param>
    public record GetAllTransactionsOfUserQuery(Guid accountId) : IRequest<List<Transaction>>;
}
