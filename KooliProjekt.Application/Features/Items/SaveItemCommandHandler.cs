using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Items
{
    public class SaveItemCommandHandler : IRequestHandler<SaveItemCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveItemCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveItemCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var item = new Item();

            if (request.Id == 0)
            {
                await _dbContext.Items.AddAsync(item);
            }
            else
            {
                item = await _dbContext.Items.FindAsync(request.Id);
            }

            item.InvoiceId = request.InvoiceId;
            item.Invoice = request.Invoice;
            item.Name = request.Name;
            item.Description = request.Description;
            item.Quantity = request.Quantity;
            item.UnitPrice = request.UnitPrice;




            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
