using SampleBank.Domain.Entities;
using MediatR;
using SampleBank.Domain.Entities;

namespace SampleBank.Application.Queries
{
    /// <summary>
    /// Record for GetTransactionInfo
    /// </summary>
    /// <param name="referenceId"></param>
    public record GetTransactionInfoQuery(string referenceId) : IRequest<Transaction>;
}
