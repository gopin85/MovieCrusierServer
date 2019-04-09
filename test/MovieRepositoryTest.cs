using moviecrusier.Data.Models;
using moviecrusier.Data.Persistence;
using System.Collections.Generic;
using Xunit;

namespace test
{
    public class MovieRepositoryTest : IClassFixture<DatabaseFixture>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly DatabaseFixture _databaseFixture;

        public MovieRepositoryTest(DatabaseFixture databaseFixture)
        {
            _databaseFixture = databaseFixture;
            _movieRepository = new MovieRepository(_databaseFixture.dbcontext);
        }

        [Fact]
        public void GetAllMovies_ShouldReturnListOfMovies()
        {
            var actual = _movieRepository.GetAllMovies();
            Assert.IsAssignableFrom<List<Movie>>(actual);
            Assert.True(actual.Count >= 3);
        }

        [Fact]
        public void GetMovie_ShouldReturnMovie()
        {
            var actual = _movieRepository.GetMovie(123);
            Assert.IsAssignableFrom<Movie>(actual);
            Assert.Equal("Iron Man1", actual.Title);
        }

        [Fact]
        public void AddMovie_MovieAdded()
        {
            var movie = new Movie { Id = 127, Title = "Iron Man7", PosterPath = "IronMan7.jpg" };
            _movieRepository.AddMovie(movie);
            var savedMovie = _movieRepository.GetMovie(127);
            Assert.Equal("Iron Man7", savedMovie.Title);
        }

        [Fact]
        public void EditMovie_MovieEdited()
        {
            var movie = new Movie { Id = 123, Title = "IronMan1", PosterPath = "IronMan1.jpg" };
            _movieRepository.AddMovie(movie);
            _movieRepository.EditMovie(123, "Update Comments");
            var savedMovie = _movieRepository.GetMovie(movie.Id);
            Assert.Equal("Update Comments", savedMovie.Comments);
        }

        [Fact]
        public void DeleteMovie_MovieDeleted()
        {
            _movieRepository.DeleteMovie(123);
            Assert.Null(_movieRepository.GetMovie(123));
        }
    }
}
