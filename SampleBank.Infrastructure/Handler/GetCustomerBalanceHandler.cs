using MediatR;
using SampleBank.Application.Queries;

namespace SampleBank.Infrastructure.Handler
{
    /// <summary>
    /// Handler for GetCustomerBalance
    /// </summary>
    public class GetCustomerBalanceHandler : IRequestHandler<GetCustomerBalanceQuery, decimal>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetCustomerBalanceHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<decimal> Handle(GetCustomerBalanceQuery request, CancellationToken cancellationToken)
        {
            var account = _dbContext.Accounts.FirstOrDefault(x => x.UserId == request.customerId);
            return Task.FromResult(account.AccountBalance);
        }
    }
}
