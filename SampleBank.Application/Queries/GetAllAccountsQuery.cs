using MediatR;
using SampleBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBank.Application.Queries
{
    public record GetAllAccountsQuery() : IRequest<List<Account>>;
}
