using Microsoft.EntityFrameworkCore;
using Xapp.Domain.Entities;

namespace Xapp.API.Data
{
    public class DbService : DbContext
    {
        public DbService(DbContextOptions<DbService> options) : base(options)
        {

        }

        public DbSet<User> Countries { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Event> Eventos { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PTO> PTOs { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<UserRol> UserRoles { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

    }
}
