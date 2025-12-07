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
        protected readonly ApplicationDbContext DbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<T> GetByIdAsync(int Id)
        {
            return await DbContext.Set<T>().FindAsync(Id);
        }

        public async Task Save(T entity)
        {
            if (entity.Id == 0)
            {
                DbContext.Set<T>().Add(entity);
            }
            else
            {
                DbContext.Set<T>().Update(entity);
            }
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var entity = await GetByIdAsync(Id);
            if (entity != null)
            {
                DbContext.Set<T>().Remove(entity);
                await DbContext.SaveChangesAsync();
            }
        }
    }
    
}
