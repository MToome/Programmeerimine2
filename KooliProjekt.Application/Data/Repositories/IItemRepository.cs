using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IItemRepository
    {
        Task<Data.Item> GetByIdAsync(int Id);
        Task SaveAsync(Item item);
        Task DeleteAsync(Item entity);
    }
}
