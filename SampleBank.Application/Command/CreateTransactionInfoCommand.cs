using SampleBank.Domain.Entities;
using MediatR;
using SampleBank.Domain.Entities;

namespace SampleBank.Application.Command
{
    /// <summary>
    /// Record for Create TransactionInfo Command
    /// </summary>
    /// <param name="transactionInfo"></param>
    public record CreateTransactionInfoCommand(Transaction transaction) : IRequest;
}
