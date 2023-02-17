using MediatR;
using SampleBank.Application.Command;
using System.Transactions;

namespace SampleBank.Infrastructure.Handler
{
    /// <summary>
    /// Handler for UpdateAccount
    /// </summary>
    public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand, Unit>
    {
        private readonly ApplicationDbContext _dbContext;

        public UpdateAccountHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Unit> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _dbContext.Accounts.Update(request.account);
                _dbContext.SaveChanges();
                transactionScope.Complete();
            }
            return Task.FromResult(Unit.Value);
        }
    }
}
