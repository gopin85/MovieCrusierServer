using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using moviecrusier.Data.Models;
using moviecrusier.Services;

namespace moviecrusier.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// To get all the movies
        /// </summary>
        /// <returns>The list of movies</returns>
        [HttpGet]
        public IActionResult Get(int pageNo, int resultPerPage)
        {

            try
            {
                var serviceResult = _movieService.GetAllMovies();
                if (pageNo == 0 && resultPerPage == 0)
                {
                    return Ok(serviceResult);
                }
                if (serviceResult != null && serviceResult.Count > 0)
                {
                    var take = resultPerPage;
                    var skip = pageNo-1 * resultPerPage;
                    if (serviceResult.Count > take && serviceResult.Count - skip < take)
                    {
                        take = serviceResult.Count - skip;
                    }
                    var serviceSortResults = serviceResult.Skip((skip)).Take(take).ToList();
                   
                    return Ok(serviceSortResults);
                }
                return Ok(new List<Movie>());
            }
            catch
            {
                return StatusCode(500, "Error occured while processing your request");
            }
        }
            

        /// <summary>
        /// To get the movie details by id
        /// </summary>
        /// <param name="id">Id of the movie</param>
        /// <returns>Movie Details</returns>
        [HttpGet("{id}")]
        public IActionResult GetMovie([FromRoute]int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest();
                }
                var movie = _movieService.GetMovie(id);
                return Ok(movie);
            }
            catch
            {
                return StatusCode(500, "Error occured while processing your request");
            }
        }

        /// <summary>
        /// Insert a movie into database 
        /// </summary>
        /// <param name="movie">Details of the movie</param>
        /// <returns>Movie inserted status with all movies</returns>
        [HttpPost]
        public ActionResult PostMovie([FromBody]Movie movie)
        {
            try
            {
                if (!ModelState.IsValid || movie == null || string.IsNullOrEmpty(movie.Title))
                {
                    return BadRequest();
                }

                var result = _movieService.AddMovie(movie);

                if (result > 0)
                {
                    return Created("Movie", _movieService.GetAllMovies());
                }
                return StatusCode(500, "Error occured while adding your movie");

            }
            catch
            {
                return StatusCode(500, "Error occured while adding your movie");
            }
        }        

        /// <summary>
        /// Updating a movie comments into database by id with comments
        /// </summary>
        /// <returns> Movie comments updated status with movie </returns>
        [HttpPut("id")]
        public IActionResult Put(int id, string comments)
        {
            try
            {
                if (id < 1 || string.IsNullOrEmpty(comments))
                {
                    return BadRequest();
                }

                var result = _movieService.EditMovie(id, comments);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "Error occured while processing your movie request");
            }
        }
        
        /// <summary>
        /// Delete a movie from database by id
        /// </summary>
        /// <returns> if movie deleted then returns remaining movie list </returns>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest();
                }
                bool result = _movieService.DeleteMovie(id);
                return Ok(_movieService.GetAllMovies());
            }
            catch
            {
                return StatusCode(500, "Error occured while processing your movie request");
            }
        }
    }
}
