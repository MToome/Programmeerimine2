using KooliProjekt.Application.DTO;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Customers
{
    [ExcludeFromCodeCoverage]
    public class GetCustomerQuery : IRequest<OperationResult<CustomerDetailsDto>>
    {
        public int Id { get; set; }

    }
}
