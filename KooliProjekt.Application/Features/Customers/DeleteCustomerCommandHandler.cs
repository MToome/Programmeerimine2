using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Customers
{
    // 15.11
    // Kustutamise käsu händler
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteCustomerCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            // Kustutamine üle relatsioonide (CASCADE DELETE)
            //await _dbContext
            //    .Customers
            //    .Where(c => c.Id == request.Id)
            //    .ExecuteDeleteAsync();

            var invoiceIds = await _dbContext
                .Invoices
                .Where(c => c.CustomerId == request.Id)
                .Select(i => i.Id)
                .ToListAsync();

            await _dbContext
                .Items
                .Where(c => invoiceIds.Contains(c.InvoiceId))
                .ExecuteDeleteAsync();


            await _dbContext
                .Invoices
                .Where(c => c.CustomerId == request.Id)
                .ExecuteDeleteAsync();

            await _dbContext
                .Customers
                .Where(c => c.Id == request.Id)
                .ExecuteDeleteAsync();

            return result;
        }
    }
}
