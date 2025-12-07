using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        
        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext) // anname baasklassile edasi DbContexti
        {
            
        }
    }
}
