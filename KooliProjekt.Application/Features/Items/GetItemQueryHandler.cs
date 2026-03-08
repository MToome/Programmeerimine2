using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTO;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Items
{
    public class GetItemQueryHandler : IRequestHandler<GetItemQuery, OperationResult<ItemDetailDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetItemQueryHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            } 
            _dbContext = dbContext;
        }

        public async Task<OperationResult<ItemDetailDto>> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<ItemDetailDto>();

            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Id <= 0)
            {
                return result;
            }

            result.Value = await _dbContext
                .Items
                .Where(item => item.Id == request.Id)
                .Select(item => new ItemDetailDto
                {
                    Id = item.Id,
                    InvoiceId = item.InvoiceId,
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
