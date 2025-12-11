using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<Data.Customer> GetByIdAsync(int Id);
        Task SaveAsync(Customer customer);
        Task DeleteAsync(Customer entity);
    }
}
