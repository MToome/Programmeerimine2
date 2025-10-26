using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Customers
{
    public class ListCustomerQueryHandler : IRequestHandler<ListCustomerQuery, OperationResult<IList<Customer>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListCustomerQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<IList<Customer>>> Handle(ListCustomerQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IList<Customer>>();
            result.Value = await _dbContext
                .Customers
                .OrderBy(list => list.Name)
                .ToListAsync();

            return result;
        }
    }
}
