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
    public class GetAllAccountsHandler : IRequestHandler<GetAllAccountsQuery, List<Account>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAllAccountsHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<List<Account>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            var userAccounts = _dbContext.Accounts.ToList();
            return Task.FromResult(userAccounts);
        }
    }
}
