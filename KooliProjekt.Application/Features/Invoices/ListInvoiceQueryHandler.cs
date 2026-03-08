using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Invoices
{
    public class ListInvoiceQueryHandler : IRequestHandler<ListInvoiceQuery, OperationResult<PagedResult<Invoice>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListInvoiceQueryHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Invoice>>> Handle(ListInvoiceQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult<PagedResult<Invoice>>();

            if (request.Page <= 0 || request.PageSize <= 0)
            {
                return result;
            }

            var query = _dbContext.Invoices.AsQueryable();

            if (!int.IsNegative(request.CustomerId))
            {
                query = query.Where(invoice => invoice.CustomerId.Equals(request.CustomerId));
            }

            if (!string.IsNullOrEmpty(request.Customer))
            {
                query = query.Where(invoice => invoice.Customer.Equals(request.Customer));
            }


            result.Value = await _dbContext
                .Invoices
                .OrderBy(list => list.DueDate)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
   
}
