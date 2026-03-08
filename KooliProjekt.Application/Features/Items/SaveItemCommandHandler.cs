using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Items
{
    public class SaveItemCommandHandler : IRequestHandler<SaveItemCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveItemCommandHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveItemCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult();

            if (request.Id < 0) 
            {
                return result.AddPropertyError(nameof(request.Id), "Id cannot be negative.");
            }

            var item = new Item();
            if (request.Id == 0)
            {
                await _dbContext.Items.AddAsync(item);
            }
            else
            {
                item = await _dbContext.Items.FindAsync(request.Id);
                if(item == null)
                {
                    return result.AddPropertyError(nameof(request.Id), "Item with the specified Id does not exist.");
                }
            }

            item.InvoiceId = request.InvoiceId;
            
            item.Name = request.Name;
            item.Description = request.Description;
            item.Quantity = request.Quantity;
            item.UnitPrice = request.UnitPrice;




            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
