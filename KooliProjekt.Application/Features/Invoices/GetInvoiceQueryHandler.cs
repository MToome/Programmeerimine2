using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Invoices
{
    public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetInvoiceQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            result.Value = await _dbContext
                .Invoices
                .Where(Invoices => Invoices.Id == request.Id)
                .Select(invoice => new
                {
                    invoice.Id,
                    invoice.Date,
                    invoice.DueDate,
                    invoice.CustomerId,
                    invoice.Items,
                    Customer = new
                    {
                        invoice.Customer.Id,
                        invoice.Customer.Name,
                        invoice.Customer.Email
                    }
                }
                )
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
