using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Customers;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Items
{
    public class ListItemQueryHandler : IRequestHandler<ListItemQuery, OperationResult<IList<Item>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListItemQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<IList<Item>>> Handle(ListItemQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IList<Item>>();
            result.Value = await _dbContext
                .Items
                .OrderBy(list => list.Name)
                .ToListAsync();

            return result;
        }
    }

}
