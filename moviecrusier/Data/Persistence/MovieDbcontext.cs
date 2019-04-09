using Microsoft.EntityFrameworkCore;
using moviecrusier.Data.Models;

namespace moviecrusier.Data.Persistence
{
    public class MovieDbContext : DbContext, IMovieDbContext
    {
        public MovieDbContext() { }

        public MovieDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
    }
}
