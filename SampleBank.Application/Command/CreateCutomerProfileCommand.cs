using MediatR;
using SampleBank.Domain.Entities;

namespace SampleBank.Application.Command
{
    /// <summary>
    /// Record for Create Cutomer Profile Command
    /// </summary>
    /// <param name="customer"></param>
    public record CreateCutomerProfileCommand(Customer customer) : IRequest;
}
