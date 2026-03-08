using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Customers;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
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
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Item>>> Handle(ListItemQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult<PagedResult<Item>>();

            if (request.Page <= 0 || request.PageSize <= 0)
            {
                return result;
            }

            var query = _dbContext.Items.AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(item => item.Name.Contains(request.Name));
            }

            result.Value = await _dbContext
                .Items
                .OrderBy(item => item.Name)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }

}
