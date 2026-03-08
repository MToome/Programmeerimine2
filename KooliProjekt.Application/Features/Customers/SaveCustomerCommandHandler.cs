using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections;
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
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveCustomerCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult();
            if (request.Id < 0)
            {
                return result.AddPropertyError(nameof(request.Id), "Id cannot be negative.");
            }

            var customer = new Customer();

            if (request.Id == 0)
            {
                await _dbContext.Customers.AddAsync(customer);
            }
            else
            {
                customer = await _dbContext.Customers.FindAsync(request.Id);
                if (customer == null)
                {
                    return result.AddPropertyError(nameof(request.Id), "ToDoList with the specified Id does not exist.");
                }
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
