using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using AuthServer.Data.Models;

namespace AuthServer.Data.Persistence
{
    public interface IUserDbContext
    {
        DbSet<User> Users { get; set; }

        int SaveChanges();

        EntityEntry Entry(object entity);
    }
}
