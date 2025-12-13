using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Customers
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, OperationResult<object>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<OperationResult<object>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var customer = await _customerRepository.GetByIdAsync(request.Id);

            result.Value = new // anonymous object
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                City = customer.City,
                Email = customer.Email,
                Phone = customer.Phone,
                Discount = customer.Discount,
            };

            return result;
        }
    }
}
