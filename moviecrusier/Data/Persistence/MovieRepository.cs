using Microsoft.EntityFrameworkCore;
using moviecrusier.Data.Models;
using System;
using System.Linq;
using System.Collections.Generic;


namespace moviecrusier.Data.Persistence
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IMovieDbContext _dbContext;

        public MovieRepository(IMovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get all movies from Database
        /// </summary>
        /// <returns>All available movies</returns>
        public List<Movie> GetAllMovies()
        {
            return _dbContext.Movies.ToList();
        }

        /// <summary>
        /// Get a movie for the requested id
        /// </summary>
        /// <param name="id">movie id</param>
        /// <returns>Movie for the requested id</returns>
        public Movie GetMovie(int id)
        {
            var movie = GetMovieById(id);
            if (movie != null)
            {
                return movie;
            }
            return null;
        }

        /// <summary>
        /// Insert a new movie into Database
        /// </summary>
        /// <param name="movie">movie details</param>
        /// <returns>inserted status</returns>
        public int AddMovie(Movie movie)
        {
            try
            {
                _dbContext.Movies.Add(movie);
                var saveChangesStateValue = _dbContext.SaveChanges();

                return saveChangesStateValue;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Removing the movie for the requested id
        /// </summary>
        /// <param name="id">movie id</param>
        /// <returns> movie removed status</returns>
        public bool DeleteMovie(int id)
        {
            var movie = GetMovieById(id);
            if (movie != null)
            {
                _dbContext.Movies.Remove(movie);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Update the movie comments for the reuested id
        /// </summary>
        /// <param name="id">Movie id</param>
        /// <param name="comments">Movie comments</param>
        /// <returns>updated movie comments </returns>
        public Movie EditMovie(int id, string comments)
        {
            var movie = GetMovieById(id);
            if (movie != null)
            {
                movie.Comments = comments;
                _dbContext.SaveChanges();
            }
            return movie;
        }

        /// <summary>
        /// Returns true if movie exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool MovieExists(int id)
        {
            var isMovieExists = _dbContext.Movies.Any(e => e.Id == id);
            return isMovieExists;
        }

        /// <summary>
        /// Returns the movie for the requested id
        /// </summary>
        /// <param name="id">movie id</param>
        /// <returns>movie for the id</returns>
        private Movie GetMovieById(int id)
        {
            if (_dbContext.Movies != null)
            {
                return _dbContext.Movies.FirstOrDefault(x => x.Id == id);
            }
            return null;
        }
    }
}
