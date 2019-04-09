using Moq;
using moviecrusier.Data.Models;
using moviecrusier.Data.Persistence;
using moviecrusier.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace test
{
    public class MovieServiceTest
    {
        [Fact]
        public void GetAllMovies_ShouldReturnListOfMovies()
        {

            //Arrange
            var mockRepo = new Mock<IMovieRepository>();
            mockRepo.Setup(repo => repo.GetAllMovies()).Returns(GetMovies());
            var service = new MovieService(mockRepo.Object, null);

            //Act
            var actual = service.GetAllMovies();

            //Assert
            Assert.IsAssignableFrom<List<Movie>>(actual);
            Assert.NotNull(actual);
            Assert.Equal(4, actual.Count);
        }

        [Fact]
        public void GetMovie_ReturnAMovie()
        {
            var mockRepo = new Mock<IMovieRepository>();
            mockRepo.Setup(repo => repo.GetMovie(123)).Returns(GetMovies().Single(m => m.Id == 123));
            var service = new MovieService(mockRepo.Object, null);
            var actual = service.GetMovie(123);
            Assert.IsAssignableFrom<Movie>(actual);
            Assert.Equal("Iron Man1", actual.Title);
        }

        [Fact]
        public void AddMovie_MovieAdded()
        {
            var mockRepo = new Mock<IMovieRepository>();
            var movie = new Movie { Id = 123, Title = "Iron Man1", PosterPath = "IronMan1.jpg" };
            List<Movie> addedMovies = new List<Movie>();
            mockRepo.Setup(repo => repo.AddMovie(movie)).Callback<Movie>((m) => addedMovies.Add(m));
            var service = new MovieService(mockRepo.Object, null);
            service.AddMovie(movie);
            Assert.True(1 == addedMovies.Count);
            Assert.NotNull(addedMovies.SingleOrDefault(m => m.Title == "Iron Man1"));
        }

        [Fact]
        public void EditMovie_MovieEdited()
        {
            var movieid = 123;
            var comments = "Good movie";
            var existingMovie = GetMovies().FirstOrDefault(x => x.Id == movieid);
            if (existingMovie != null)
            {
                existingMovie.Comments = comments;
            }
            var mockRepo = new Mock<IMovieRepository>();
            mockRepo.Setup(x => x.EditMovie(movieid, comments)).Returns(existingMovie);
            var service = new MovieService(mockRepo.Object, null);
            var actual = service.EditMovie(movieid, comments);
            Assert.NotNull(actual);
        }


        [Fact]
        public void EditMovie_MovieNotEdited()
        {
            var movieid = 12345;
            var comments = "Good movie";
            var existingMovie = GetMovies().FirstOrDefault(x => x.Id == movieid);
            if (existingMovie != null)
            {
                existingMovie.Comments = comments;
            }
            var mockRepo = new Mock<IMovieRepository>();
            mockRepo.Setup(x => x.EditMovie(movieid, comments)).Returns(existingMovie);
            var service = new MovieService(mockRepo.Object, null);
            var actual = service.EditMovie(movieid, comments);
            Assert.Null(actual);
        }

        [Fact]
        public void DeleteMovie_MovieIsDeleted()
        {
            var mockRepo = new Mock<IMovieRepository>();
            var movie = new Movie { Id = 123, Title = "Iron Man1", PosterPath = "IronMan1.jpg" };
            List<Movie> addedMovies = new List<Movie> { movie };
            mockRepo.Setup(repo => repo.GetMovie(movie.Id)).Returns(movie);
            mockRepo.Setup(repo => repo.DeleteMovie(movie.Id)).Callback<int>((id) => addedMovies.Remove(addedMovies.Single(m => m.Id == id)));
            var service = new MovieService(mockRepo.Object, null);
            service.DeleteMovie(movie.Id);
            Assert.True(0 == addedMovies.Count);
        }
       

        private List<Movie> GetMovies()
        {
            var movies = new List<Movie>
            {
                new Movie { Id = 123, Budget=100000, Title = "Iron Man1", PosterPath = "IronMan1.jpg", VoteAverage=25, VoteCount=25 },
                new Movie { Id = 124, Budget=100000, Title = "Iron Man2", PosterPath = "IronMan2.jpg", VoteAverage=25, VoteCount=25 },
                new Movie { Id = 125, Budget=100000, Title = "Iron Man3", PosterPath = "IronMan3.jpg", VoteAverage=25, VoteCount=25 },
                new Movie { Id = 126, Budget=100000, Title = "Iron Man4", PosterPath = "IronMan4.jpg", VoteAverage=25, VoteCount=25 }
            };

            return movies;

        }

    }
}
