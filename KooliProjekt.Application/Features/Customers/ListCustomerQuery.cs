using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.Collections.Generic;

namespace KooliProjekt.Application.Features.Customers
{
    public class ListCustomerQuery : IRequest<OperationResult<IList<Customer>>>
    {
    }
}
