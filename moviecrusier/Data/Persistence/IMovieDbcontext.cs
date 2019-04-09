using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using moviecrusier.Data.Models;

namespace moviecrusier.Data.Persistence
{
    public interface IMovieDbContext
    {
        DbSet<Movie> Movies { get; set; }

        int SaveChanges();

        EntityEntry Entry(object entity);
    }
}
