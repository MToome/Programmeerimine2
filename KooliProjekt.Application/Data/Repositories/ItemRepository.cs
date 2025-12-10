using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        
        public ItemRepository(ApplicationDbContext dbContext) : base(dbContext) // anname baasklassile edasi DbContexti
        {
            
        }
    }
}
