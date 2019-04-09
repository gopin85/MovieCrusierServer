using Microsoft.AspNetCore.Mvc;
using Moq;
using moviecrusier.Controllers;
using moviecrusier.Data.Models;
using moviecrusier.Services;
using System.Collections.Generic;
using Xunit;
using System;
using System.Linq;
using System.Collections;

namespace MoviesCruiserTest
{
    public class MovieControllerTest
    {

        [Fact]
        public void GetMethodWithoutParameter_ShouldReturnListOfMovie()
        {
            var mockService = new Mock<IMovieService>();
            mockService.Setup(x => x.GetAllMovies()).Returns(GetAllMovies());
            var controller = new MovieController(mockService.Object);

            var result = controller.Get(1, 5);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<IEnumerable<Movie>>(actionResult.Value);
            Assert.NotNull(response);
            Assert.Equal(200, actionResult.StatusCode);
        }

        [Fact]
        public void GetMethodWithoutParameter_ShouldNotReturnListOfMovie()
        {
            var mockService = new Mock<IMovieService>();
            mockService.Setup(x => x.GetAllMovies()).Returns(new List<Movie>());
            var controller = new MovieController(mockService.Object);

            var result = controller.Get(1, 5);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<IEnumerable<Movie>>(actionResult.Value);
            Assert.NotNull(response);
            Assert.False(response.Any());
            Assert.Equal(200, actionResult.StatusCode);
        }

        [Fact]
        public void GetMethodWithoutParameter_ShouldReturnBadRequest()
        {
            var mockService = new Mock<IMovieService>();
            mockService.Setup(x => x.GetAllMovies()).Returns(new List<Movie>());
            var controller = new MovieController(mockService.Object);

            var result = controller.Get(1, 5);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, actionResult.StatusCode);
        }

        [Fact]
        public void GetMethodWithParameter_ShouldReturnMovie()
        {
            int movieId = 123;
            var mockService = new Mock<IMovieService>();
            mockService.Setup(x => x.GetMovie(movieId)).Returns(GetAllMovies().FirstOrDefault(x => x.Id == movieId));
            var controller = new MovieController(mockService.Object);

            var result = controller.GetMovie(movieId);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<Movie>(actionResult.Value);

            Assert.NotNull(response);
            Assert.Equal(movieId, response.Id);
            Assert.Equal(200, actionResult.StatusCode);
        }

        [Fact]
        public void GetMethodWithParameter_ShouldNotReturnMovie()
        {
            int movieId = 72;
            var mockService = new Mock<IMovieService>();
            mockService.Setup(x => x.GetMovie(movieId)).Returns(GetAllMovies().FirstOrDefault(x => x.Id == movieId));
            var controller = new MovieController(mockService.Object);
            var result = controller.GetMovie(movieId);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, actionResult.StatusCode);
        }

        [Fact]
        public void GetMethodWithParameter_ShouldReturnBadRequest()
        {
            var movieId = 0;
            var mockService = new Mock<IMovieService>();
            mockService.Setup(x => x.GetMovie(movieId)).Returns((Movie)null);
            var controller = new MovieController(mockService.Object);

            var result = controller.GetMovie(movieId);

            //Assert
            var actionResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, actionResult.StatusCode);
        }

        [Fact]
        public void Post_ShouldReturnBadRequest()
        {
            var movie = (Movie)null;
            var expected = 0;
            var mockService = new Mock<IMovieService>();
            mockService.Setup(x => x.AddMovie(movie)).Returns(expected);
            var controller = new MovieController(mockService.Object);

            var result = controller.PostMovie(movie);

            //Assert
            var actionResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, actionResult.StatusCode);
        }


        [Fact]
        public void Post_ShouldAddMovie()
        {
            var existingMovies = GetAllMovies();
            var totalMoviesActual = existingMovies.Count;
            var newMovie = new Movie
            {
                Id = 159,
                Title = "Iron Man5",
                PosterPath = "IronMan5",
                Comments = "Good",
                ReleaseDate = "Monday",
                VoteAverage = 1,
                VoteCount = 45
            };
            existingMovies.Add(newMovie);

            var mockService = new Mock<IMovieService>();
            mockService.Setup(x => x.AddMovie(newMovie)).Returns(1);
            var controller = new MovieController(mockService.Object);

            var result = controller.PostMovie(newMovie);

            //Assert
            var actionResult = Assert.IsType<CreatedResult>(result);
            var response = Assert.IsAssignableFrom<List<Movie>>(existingMovies);

            Assert.NotNull(response);
            Assert.True(response.Count > totalMoviesActual);
            Assert.Equal(201, actionResult.StatusCode);
        }

        [Fact]
        public void Put_ShouldReturnBadRequest()
        {
            var mockMovieId = 0;
            var comments = "";
            var mockService = new Mock<IMovieService>();
            mockService.Setup(x => x.EditMovie(It.IsAny<int>(), It.IsAny<string>())).Returns(new Movie());
            var controller = new MovieController(mockService.Object);

            var result = controller.Put(mockMovieId, comments);

            //Assert
            var actionResult = Assert.IsType<BadRequestResult>(result);

            Assert.Equal(400, actionResult.StatusCode);
        }


        [Fact]
        public void Put_ShouldUpdateMovie()
        {
            var mockMovieId = 123;
            var comments = "Fantastic movie to watch";
            var existingMovies = GetAllMovies();
            var movieToBeUpdated = existingMovies.FirstOrDefault(x => x.Id == mockMovieId);
            movieToBeUpdated.Comments = comments;
            var mockService = new Mock<IMovieService>();
            mockService.Setup(x => x.EditMovie(It.IsAny<int>(), It.IsAny<string>())).Returns(movieToBeUpdated);
            var controller = new MovieController(mockService.Object);

            var result = controller.Put(mockMovieId, comments);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<Movie>(actionResult.Value);

            Assert.NotNull(response);
            Assert.Equal(200, actionResult.StatusCode);
        }

        [Fact]
        public void Put_ShouldNotUpdateMovie()
        {
            var mockMovieId = 128;
            var comments = "Fantastic movie to watch";
            var expected = (Movie)null;
            var mockService = new Mock<IMovieService>();
            mockService.Setup(x => x.EditMovie(It.IsAny<int>(), It.IsAny<string>())).Returns(expected);
            var controller = new MovieController(mockService.Object);

            var result = controller.Put(mockMovieId, comments);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, actionResult.StatusCode);
        }

        [Fact]
        public void Delete_ShouldReturnBadRequest()
        {
            var expected = false;
            var mockService = new Mock<IMovieService>();
            mockService.Setup(x => x.DeleteMovie(It.IsAny<int>())).Returns(expected);
            var controller = new MovieController(mockService.Object);

            var result = controller.Delete(0);

            //Assert
            var actionResult = Assert.IsType<BadRequestResult>(result);

            Assert.Equal(400, actionResult.StatusCode);
        }

        [Fact]
        public void Delete_ShouldDeleteMovieForGivenId()
        {
            var allMovies = GetAllMovies();
            var beforeDeleted = allMovies.Count;
            var expected = allMovies.Remove(allMovies.FirstOrDefault(x => x.Id == 126));
            var actualCount = allMovies.Count;
            var mockService = new Mock<IMovieService>();
            mockService.Setup(x => x.DeleteMovie(It.IsAny<int>())).Returns(expected);
            var controller = new MovieController(mockService.Object);

            var result = controller.Delete(126);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsAssignableFrom<List<Movie>>(allMovies);
            Assert.Equal(200, actionResult.StatusCode);
            Assert.NotEqual(beforeDeleted, actualCount);
        }

        [Fact]
        public void Delete_ShouldNotDeleteMovieForGivenId()
        {
            var expected = false;
            var mockService = new Mock<IMovieService>();
            mockService.Setup(x => x.DeleteMovie(It.IsAny<int>())).Returns(expected);
            var controller = new MovieController(mockService.Object);

            var result = controller.Delete(126);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, actionResult.StatusCode);
        }


        private List<Movie> GetAllMovies()
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
