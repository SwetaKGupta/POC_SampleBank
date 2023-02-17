using MediatR;
using SampleBank.Application.Queries;
using SampleBank.Domain.Entities;

namespace SampleBank.Infrastructure.Handler
{
    /// <summary>
    /// Handler for GetAllTransactions
    /// </summary>
    public class GetAllTransactionsOfUserHandler : IRequestHandler<GetAllTransactionsOfUserQuery, List<Transaction>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAllTransactionsOfUserHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<List<Transaction>> Handle(GetAllTransactionsOfUserQuery request, CancellationToken cancellationToken)
        {
            var transactionList = _dbContext.Transactions.Where(x => x.SourceAccountId == request.accountId).ToList();
            return Task.FromResult(transactionList);
        }
    }
}
