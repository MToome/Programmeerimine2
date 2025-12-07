using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<Data.Customer> GetByIdAsync(int Id);
        Task Save(Customer customer);
        Task Delete(int Id);
    }
}
