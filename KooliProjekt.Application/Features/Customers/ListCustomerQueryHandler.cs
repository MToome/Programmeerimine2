using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Customers
{
    public class ListCustomerQueryHandler : IRequestHandler<ListCustomerQuery, OperationResult<PagedResult<Customer>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListCustomerQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Customer>>> Handle(ListCustomerQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Customer>>();
            result.Value = await _dbContext
                .Customers
                .OrderBy(list => list.Name)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
