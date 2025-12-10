using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IInvoiceRepository
    {
        Task<Data.Invoice> GetByIdAsync(int Id);
        Task Save(Invoice invoice);
        Task Delete(int Id);
    }
}
