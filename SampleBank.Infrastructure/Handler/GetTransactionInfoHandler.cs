using MediatR;
using SampleBank.Application.Queries;
using SampleBank.Domain.Entities;

namespace SampleBank.Infrastructure.Handler
{
    /// <summary>
    ///  Handler for GetTransactionInfo
    /// </summary>
    public class GetTransactionInfoHandler : IRequestHandler<GetTransactionInfoQuery, Transaction>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetTransactionInfoHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Transaction> Handle(GetTransactionInfoQuery request, CancellationToken cancellationToken)
        {
            var transaction = _dbContext.Transactions.FirstOrDefault(x => x.TransactionReference == request.referenceId);
            return Task.FromResult(transaction);
        }
    }
}
