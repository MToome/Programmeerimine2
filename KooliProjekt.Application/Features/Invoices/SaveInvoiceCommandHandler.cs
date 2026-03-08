using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Invoices
{
    public class SaveInvoiceCommandHandler : IRequestHandler<SaveInvoiceCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveInvoiceCommandHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            { 
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveInvoiceCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult();

            var invoice = new Invoice();

            if (request.Id < 0)
            {
                return result.AddPropertyError(nameof(request.Id), "Id cannot be negative.");
            }

            if (request.Id == 0)
            {
                await _dbContext.Invoices.AddAsync(invoice);
            }
            else
            {
                invoice = await _dbContext.Invoices.FindAsync(request.Id);
                if (invoice == null)
                {
                    return result.AddPropertyError(nameof(request.Id), "Invoice not found.");
                }
            }

            invoice.Date = request.Date;
            invoice.DueDate = request.DueDate;
            invoice.CustomerId = request.CustomerId;
            invoice.Customer = await _dbContext.Customers.FindAsync(request.CustomerId);
            invoice.Items = await _dbContext.Items
                .Where(i => request.Items.Contains(i.Id))
                .ToListAsync(cancellationToken);




            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
