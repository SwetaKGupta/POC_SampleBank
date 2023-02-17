using MediatR;
using SampleBank.Application.Queries;
using SampleBank.Domain.Entities;

namespace SampleBank.Infrastructure.Handler
{
    public class GetAccountByAccountIdHandler : IRequestHandler<GetAccountByAccountIdQuery, Account>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAccountByAccountIdHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Account> Handle(GetAccountByAccountIdQuery request, CancellationToken cancellationToken)
        {
            var userAccount = _dbContext.Accounts.FirstOrDefault(x => x.Id == request.accountId);
            return Task.FromResult(userAccount);
        }
    }
}
