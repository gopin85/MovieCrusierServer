using Microsoft.EntityFrameworkCore;
using AuthServer.Data.Models;

namespace AuthServer.Data.Persistence
{
    /*User DbContext*/
    public class UserDbContext : DbContext, IUserDbContext
    {
        public UserDbContext() { }

        /* Initializing the DbContextOptions*/
        public UserDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
    }
}
