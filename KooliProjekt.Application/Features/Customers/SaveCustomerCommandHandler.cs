using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Customers
{
    public class SaveCustomerCommandHandler : IRequestHandler<SaveCustomerCommand, OperationResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public SaveCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<OperationResult> Handle(SaveCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var customer = new Customer();

            if (request.Id != 0)
            {
                customer = await _customerRepository.GetByIdAsync(request.Id);
            }
           
            customer.Name = request.Name;
            customer.Address = request.Address;
            customer.City = request.City;
            customer.Email = request.Email;
            customer.Phone = request.Phone;
            customer.Discount = request.Discount;
        
            await _customerRepository.Save(customer);
            return result;
        }
    }
}
