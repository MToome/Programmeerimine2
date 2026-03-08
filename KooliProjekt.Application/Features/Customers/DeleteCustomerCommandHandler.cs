using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTO;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
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
            if (dbContext == null)
            {
                throw new System.ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            if (request == null)
            {
                throw new System.ArgumentNullException(nameof(request));
            }

            if (request.Id <= 0)
            {
                return result;
            }

            // Kustutamine üle relatsioonide (CASCADE DELETE)
            //await _dbContext
            //    .Customers
            //    .Where(c => c.Id == request.Id)
            //    .ExecuteDeleteAsync();

            // Tõlkimis probleem, EF Core InMemory provider’it test ei toeta ExecuteDeleteAsync(),
            // ExecuteDeleteAsync() töötab ainult relatsiooniliste provideritega (nt SQL Server, PostgreSQL).
            //var invoiceIds = await _dbContext
            //    .Invoices
            //    .Where(c => c.CustomerId == request.Id)
            //    .Select(i => i.Id)
            //    .ToListAsync();

            //await _dbContext
            //    .Items
            //    .Where(c => invoiceIds.Contains(c.InvoiceId))
            //    .ExecuteDeleteAsync();


            //await _dbContext
            //    .Invoices
            //    .Where(c => c.CustomerId == request.Id)
            //    .ExecuteDeleteAsync();

            //await _dbContext
            //    .Customers
            //    .Where(c => c.Id == request.Id)
            //    .ExecuteDeleteAsync();

            //if (invoiceIds == null)
            //{
            //    return result;
            //}


            var customer = await _dbContext
                .Customers
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (customer == null)
                return result;

            // Leia seotud arved
            var invoices = await _dbContext
                .Invoices
                .Where(i => i.CustomerId == request.Id)
                .ToListAsync(cancellationToken);

            // Leia seotud read
            var invoiceIds = invoices.Select(i => i.Id).ToList();

            var items = await _dbContext
                .Items
                .Where(i => invoiceIds.Contains(i.InvoiceId))
                .ToListAsync(cancellationToken);

            _dbContext.Items.RemoveRange(items);
            _dbContext.Invoices.RemoveRange(invoices);
            _dbContext.Customers.Remove(customer);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
