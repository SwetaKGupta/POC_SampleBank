using MediatR;
using SampleBank.Application.Command;

namespace SampleBank.Infrastructure.Handler
{
    /// <summary>
    /// Handler for Create Customer Account
    /// </summary>
    public class CreateCustomerAccountHandler : IRequestHandler<CreateCustomerAccountCommand, Unit>
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateCustomerAccountHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Unit> Handle(CreateCustomerAccountCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Accounts.Add(request.account);
            _dbContext.SaveChanges();
            return Task.FromResult(Unit.Value);
        }
    }
}
