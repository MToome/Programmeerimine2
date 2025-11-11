using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
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
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Invoice>>> Handle(ListInvoiceQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Invoice>>();
            result.Value = await _dbContext
                .Invoices
                .OrderBy(list => list.DueDate)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
   
}
