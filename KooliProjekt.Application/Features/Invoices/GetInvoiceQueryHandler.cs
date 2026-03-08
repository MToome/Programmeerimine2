using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTO;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Invoices
{
    public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, OperationResult<InvoiceDetailsDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetInvoiceQueryHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new System.ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult<InvoiceDetailsDto>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<InvoiceDetailsDto>();

            if(request == null)
            {
                throw new System.ArgumentNullException(nameof(request));
            }

            if(request.Id <= 0)
            {
                return result;
            }

            result.Value = await _dbContext
                .Invoices
                .Where(invoice => invoice.Id == request.Id)
                .Select(invoice => new InvoiceDetailsDto
                {
                    Id = invoice.Id,
                    Date = invoice.Date,
                    DueDate = invoice.DueDate,
                    CustomerId = invoice.CustomerId,
                    Items = invoice.Items.Select(item => new ItemDetailDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    }).ToList(),
                    Customer = new CustomerDetailsDto
                    {
                        Id = invoice.Customer.Id,
                        Name = invoice.Customer.Name,
                        Address = invoice.Customer.Address,
                        City = invoice.Customer.City,
                        Email = invoice.Customer.Email,
                        Phone = invoice.Customer.Phone,
                        Discount = invoice.Customer.Discount
                    }
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
