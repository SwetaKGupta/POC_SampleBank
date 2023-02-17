using MediatR;

namespace SampleBank.Application.Queries
{
    /// <summary>
    /// Record for GetCustomerBalance
    /// </summary>
    /// <param name="customerId"></param>
    public record GetCustomerBalanceQuery(Guid customerId) : IRequest<decimal>;
}
