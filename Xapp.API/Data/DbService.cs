using Microsoft.EntityFrameworkCore;

namespace Xapp.API.Data
{
    public class DbService : DbContext
    {
        public DbService(DbContextOptions<DbService> options) : base(options)
        {

        }
    }
}
