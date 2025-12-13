using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {

        public InvoiceRepository(ApplicationDbContext dbContext) : base(dbContext) // anname baasklassile edasi DbContexti
        {


        }

        public override async Task<Invoice> GetByIdAsync(int id)
        {
            return await DbContext
                .Invoices
                .Include(customer => customer.Items)
                .Where(customer => customer.Id == id)
                .FirstOrDefaultAsync();

        }
    }
}
