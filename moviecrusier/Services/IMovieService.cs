using moviecrusier.Data.Models;
using System.Collections.Generic;

namespace moviecrusier.Services
{
    public interface IMovieService
    {
        List<Movie> GetAllMovies();

        Movie GetMovie(int id);

        bool MovieExists(int id);

        int AddMovie(Movie movie);

        Movie EditMovie(int id, string comments);

        bool DeleteMovie(int id);
    }
}
