using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Customers
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetCustomerQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            result.Value = await _dbContext
                .Customers
                .Where(Customer => Customer.Id == request.Id)
                .Select(Customer => new
                {
                    Id = Customer.Id,
                    Name = Customer.Name,
                    Address = Customer.Address,
                    City = Customer.City,
                    Email = Customer.Email,
                    Phone = Customer.Phone,
                    Discount = Customer.Discount
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
