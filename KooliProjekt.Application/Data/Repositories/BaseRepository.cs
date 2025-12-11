using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public abstract class BaseRepository<T> where T : Entity
    {
        protected ApplicationDbContext DbContext {get; private set; }

        public BaseRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        // CRUD
        public virtual async Task<T> GetByIdAsync(int Id)
        {
            return await DbContext.Set<T>().FindAsync(Id);
        }

        public async Task SaveAsync(T list)
        {
            if (list.Id == 0)
            {
                DbContext.Set<T>().Add(list);
            }
            else
            {
                DbContext.Set<T>().Update(list);
            }
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity != null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            
            DbContext.Set<T>().Remove(entity);
            await DbContext.SaveChangesAsync();

        }
    }
    
}
