using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Customers
{
    public class SaveCustomerCommandHandler : IRequestHandler<SaveCustomerCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveCustomerCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var customer = new Customer();

            if (request.Id == null)
            {
                await _dbContext.Customers.AddAsync(customer, cancellationToken);
            }
            else
            {
                customer = await _dbContext.Customers.FindAsync(request.Id);
            }

            customer.Name = request.Name;
            customer.Address = request.Address;
            customer.City = request.City;
            customer.Email = request.Email;
            customer.Phone = request.Phone;
            customer.Discount = request.Discount;
        
            await _dbContext.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
