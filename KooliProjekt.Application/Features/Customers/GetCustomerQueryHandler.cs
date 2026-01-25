using KooliProjekt.Application.Data;
using KooliProjekt.Application.DTO;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Customers
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, OperationResult<CustomerDetailsDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetCustomerQueryHandler(ApplicationDbContext dbContext)
        {
            if(dbContext == null)
            {
                throw new System.ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult<CustomerDetailsDto>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<CustomerDetailsDto>();

            if (request == null)
            {
                return result;
            }

            if (request.Id <= 0)
            {
                return result;
            }

            result.Value = await _dbContext
                .Customers
                .Where(Customer => Customer.Id == request.Id)
                .Select(Customer => new CustomerDetailsDto
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
