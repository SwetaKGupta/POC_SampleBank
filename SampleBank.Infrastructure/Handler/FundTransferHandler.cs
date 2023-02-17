using MediatR;
using SampleBank.Application.Command;
using System.Transactions;

namespace SampleBank.Infrastructure.Handler
{
    /// <summary>
    /// Handler for UpdateAccount
    /// </summary>
    public class FundTransferHandler : IRequestHandler<FundTransferCommand, Unit>
    {
        private readonly ApplicationDbContext _dbContext;

        public FundTransferHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Unit> Handle(FundTransferCommand request, CancellationToken cancellationToken)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _dbContext.Accounts.Update(request.fromAccount);
                _dbContext.Accounts.Update(request.toAccount);
                _dbContext.Transactions.Add(request.transaction);
                //I have added record in transaction table in this section but we can use any messsaging service like SQS
                _dbContext.SaveChanges();

                transactionScope.Complete();
            }
            return Task.FromResult(Unit.Value);
        }
    }
}
