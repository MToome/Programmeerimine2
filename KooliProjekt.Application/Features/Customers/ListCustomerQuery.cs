using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Features.Customers
{
    [ExcludeFromCodeCoverage]
    public class ListCustomerQuery : IRequest<OperationResult<PagedResult<Customer>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

    }
}
