using MediatR;
using SampleBank.Application.Command;

namespace SampleBank.Infrastructure.Handler
{
    /// <summary>
    /// Handler for Customer Profile
    /// </summary>
    public class CreateCutomerProfileHandler : IRequestHandler<CreateCutomerProfileCommand, Unit>
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateCutomerProfileHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Unit> Handle(CreateCutomerProfileCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Customers.Add(request.customer);
            _dbContext.SaveChanges();
            return Task.FromResult(Unit.Value);
        }
    }
}
