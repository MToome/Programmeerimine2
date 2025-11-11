using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Customers;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Items
{
    public class ListItemQueryHandler : IRequestHandler<ListItemQuery, OperationResult<PagedResult<Item>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListItemQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Item>>> Handle(ListItemQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Item>>();
            result.Value = await _dbContext
                .Items
                .OrderBy(list => list.Name)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }

}
