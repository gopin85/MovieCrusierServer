using Microsoft.EntityFrameworkCore;
using moviecrusier.Data.Models;
using moviecrusier.Data.Persistence;
using System;
using System.Collections.Generic;

namespace test
{
    public class DatabaseFixture : IDisposable
    {
        private IEnumerable<Movie> Movies { get; set; }
        public IMovieDbContext dbcontext;

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<MovieDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieDB")
                .Options;
            dbcontext = new MovieDbContext(options);

            dbcontext.Movies.Add(new Movie { Id = 123, Budget=100000, Title = "Iron Man1", PosterPath = "IronMan1.jpg", VoteAverage=25, VoteCount=25 });
            dbcontext.Movies.Add(new Movie { Id = 124, Budget = 100000, Title = "Iron Man2", PosterPath = "IronMan2.jpg", VoteAverage = 25, VoteCount = 25 });
            dbcontext.Movies.Add(new Movie { Id = 125, Budget = 100000, Title = "Iron Man3", PosterPath = "IronMan3.jpg", VoteAverage = 25, VoteCount = 25 });
            dbcontext.Movies.Add(new Movie { Id = 126, Budget = 100000, Title = "Iron Man4", PosterPath = "IronMan4.jpg", VoteAverage = 25, VoteCount = 25 });
            dbcontext.SaveChanges();
        }

        public void Dispose()
        {
            dbcontext = null;
        }


    }
}
