using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using moviecrusier.Data.Models;
using moviecrusier.Data.Persistence;
using System.Collections.Generic;

namespace moviecrusier.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        
        public MovieService(IMovieRepository movieRepository, IHttpContextAccessor httpContext)
        {
            _movieRepository = movieRepository;
            
        }

        /// <summary>
        /// To get all movies
        /// </summary>
        /// <returns>Returns the list of movies</returns>
        public List<Movie> GetAllMovies()
        {
            var allMovies = _movieRepository.GetAllMovies();
            return allMovies;
        }

        /// <summary>
        /// To get a particular movie
        /// </summary>
        /// <param name="id">Id of the movie</param>
        /// <returns></returns>
        public Movie GetMovie(int id)
        {
            var Movie = _movieRepository.GetMovie(id);
            return Movie;
        }

        /// <summary>
        /// To check the existence of the movie
        /// </summary>
        /// <param name="id">Id of the movie</param>
        /// <returns>Returns True or False</returns>
        public bool MovieExists(int id)
        {
            var isMovieExists = _movieRepository.MovieExists(id);
            return isMovieExists;
        }

        /// <summary>
        /// To add the movie
        /// </summary>
        /// <param name="movie">Details of the movie</param>
        /// <returns>Id of the movie</returns>
        public int AddMovie(Movie movie)
        {
           return _movieRepository.AddMovie(movie);
        }

        /// <summary>
        /// To update the comments 
        /// </summary>
        /// <param name="id">Id of the movie</param>
        /// <param name="comments">Comments</param>
        /// <returns>Details of the movie</returns>
        public Movie EditMovie(int id, string comments)
        {
            return _movieRepository.EditMovie(id, comments);
        }

        /// <summary>
        /// To delete the movie
        /// </summary>
        /// <param name="id">Id of the movie</param>
        /// <returns>Returns True or False</returns>
        public bool DeleteMovie(int id)
        {
            return _movieRepository.DeleteMovie(id);
        }

    }
}
