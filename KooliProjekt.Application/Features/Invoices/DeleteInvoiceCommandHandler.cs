using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult();

            if (request.Id <= 0)
            {
                return result;
            }

            // Kustutamine üle relatsioonide (CASCADE DELETE)
            //await _dbContext
            //    .Invoices
            //    .Where(c => c.Id == request.Id)
            //    .ExecuteDeleteAsync();

            var invoice = await _dbContext
                .Invoices
                .FirstOrDefaultAsync(t => t.Id == request.Id);

            if (invoice == null)
            {
                return result;
            }

            _dbContext.Invoices.Remove(invoice);

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
