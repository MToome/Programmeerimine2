using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Customers
{
    public class ListCustomerQueryHandler : IRequestHandler<ListCustomerQuery, OperationResult<PagedResult<Customer>>>
    {
        private readonly ApplicationDbContext _dbContext;
        public ListCustomerQueryHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Customer>>> Handle(ListCustomerQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult<PagedResult<Customer>>();

            if (request.Page <= 0 || request.PageSize <= 0)
            {
                return result;
            }

            var query = _dbContext.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(customer => customer.Name.Contains(request.Name));
            }
            
            if (!string.IsNullOrEmpty(request.Email))
            {
                query = query.Where(customer => customer.Email.Contains(request.Email));
            }

            if (!string.IsNullOrEmpty(request.Phone))
            {
                query = query.Where(customer => customer.Phone.Contains(request.Phone));
            }
            
            if (!string.IsNullOrEmpty(request.Address))
            {
                query = query.Where(customer => customer.Address.Contains(request.Address));
            }

            result.Value = await query
                .OrderBy(customer => customer.Name)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
