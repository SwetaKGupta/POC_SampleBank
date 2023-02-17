using MediatR;

namespace SampleBank.Application.Queries
{
    /// <summary>
    /// Record for GetCustomersCreatedToday
    /// </summary>
    public record GetCustomersCreatedTodayQuery() : IRequest<int>;
}
