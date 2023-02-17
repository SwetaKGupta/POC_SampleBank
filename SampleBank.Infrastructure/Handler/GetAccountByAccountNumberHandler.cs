using MediatR;
using SampleBank.Application.Queries;
using SampleBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBank.Infrastructure.Handler
{
    public class GetAccountByAccountNumberHandler : IRequestHandler<GetAccountByAccountNumberQuery, Account>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAccountByAccountNumberHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Account> Handle(GetAccountByAccountNumberQuery request, CancellationToken cancellationToken)
        {
            var userAccount = _dbContext.Accounts.FirstOrDefault(x => x.AccountNumber == request.accountNumber);
            return Task.FromResult(userAccount);
        }
    }
}
