using MediatR;
using SampleBank.Application.Queries;

namespace SampleBank.Infrastructure.Handler
{
    /// <summary>
    /// Handler for GetCustomersCreatedToday
    /// </summary>
    public class GetCustomersCreatedTodayHandler : IRequestHandler<GetCustomersCreatedTodayQuery, int>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetCustomersCreatedTodayHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<int> Handle(GetCustomersCreatedTodayQuery request, CancellationToken cancellationToken)
        {
            var count = _dbContext.Customers.Count(x => x.MemberSince > DateTime.Today);
            return Task.FromResult(count);
        }
    }
}
