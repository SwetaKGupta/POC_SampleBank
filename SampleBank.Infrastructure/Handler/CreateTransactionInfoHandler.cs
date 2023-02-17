using MediatR;
using SampleBank.Application.Command;

namespace SampleBank.Infrastructure.Handler
{
    /// <summary>
    /// Handler for TransactionInfo
    /// </summary>
    public class CreateTransactionInfoHandler : IRequestHandler<CreateTransactionInfoCommand, Unit>
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateTransactionInfoHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Unit> Handle(CreateTransactionInfoCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Transactions.Add(request.transaction);
            _dbContext.SaveChanges();
            return Task.FromResult(Unit.Value);
        }
    }
}
