using Microsoft.EntityFrameworkCore;
using Xapp.Domain.ExamplesOfEntities;

namespace Xapp.API.Data
{
    public class DbService : DbContext
    {
        public DbService(DbContextOptions<DbService> options) : base(options)
        {

        }

        //public DbSet<Country> Countries  { get; set; }
        //public DbSet<Category> Categories  { get; set; }
        //public DbSet<CountryCategories> CountryCategories { get; set; }

    }
}
