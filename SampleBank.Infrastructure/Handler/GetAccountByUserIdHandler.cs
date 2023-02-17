using MediatR;
using SampleBank.Application.Queries;
using SampleBank.Domain.Entities;

namespace SampleBank.Infrastructure.Handler
{
    /// <summary>
    /// Handler for GetAccountByUserId
    /// </summary>
    public class GetAccountByUserIdHandler : IRequestHandler<GetAccountByUserIdQuery, Account>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAccountByUserIdHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Account> Handle(GetAccountByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userAccount = _dbContext.Accounts.Single(x => x.UserId == request.userId);
            return Task.FromResult(userAccount);
        }
    }
}
