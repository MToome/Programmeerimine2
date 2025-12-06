using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Items
{
    public class GetItemQueryHandler : IRequestHandler<GetItemQuery, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetItemQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            result.Value = await _dbContext
                .Items
                .Where(item => item.Id == request.Id)
                .Select(item => new
                {
                    Id = item.Id,
                    InvoiceID = item.InvoiceId,
                    Invoice = item.Invoice,
                    Name = item.Name,
                    Description = item.Description,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }
                )

                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
