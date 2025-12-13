using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
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
        private readonly IInvoiceRepository _invoiceRepository;

        public GetInvoiceQueryHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<OperationResult<object>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var invoice = await _invoiceRepository.GetByIdAsync(request.Id);

            result.Value = new
            {
                invoice.Id,
                invoice.Date,
                invoice.DueDate,
                invoice.CustomerId,
                Items = invoice.Items.Select(item => new
                {
                    Id = item.Id,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                })

            };
            return result;
        }
    }
}
