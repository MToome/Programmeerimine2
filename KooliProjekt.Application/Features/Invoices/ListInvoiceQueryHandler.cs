using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Invoices
{
    public class ListInvoiceQueryHandler : IRequestHandler<ListInvoiceQuery, OperationResult<IList<Invoice>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListInvoiceQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<IList<Invoice>>> Handle(ListInvoiceQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IList<Invoice>>();
            result.Value = await _dbContext
                .Invoices
                .OrderBy(list => list.DueDate)
                .ToListAsync();

            return result;
        }
    }
   
}
