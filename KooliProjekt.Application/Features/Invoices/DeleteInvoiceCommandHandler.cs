using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Customers;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Invoices
{
    public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteInvoiceCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            // Kustutamine üle relatsioonide (CASCADE DELETE)
            //await _dbContext
            //    .Invoices
            //    .Where(c => c.Id == request.Id)
            //    .ExecuteDeleteAsync();

            await _dbContext
                .Items
                .Where(c => c.InvoiceId == request.Id)
                .ExecuteDeleteAsync();

            await _dbContext
                .Invoices
                .Where(c => c.Id == request.Id)
                .ExecuteDeleteAsync();

            return result;
        }
    }
}
