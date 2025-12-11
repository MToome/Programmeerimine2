namespace KooliProjekt.Application.Data.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        
        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext) // anname baasklassile edasi DbContexti
        {
            
        }
    }
}
