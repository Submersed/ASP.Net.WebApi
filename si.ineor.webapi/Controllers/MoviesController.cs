using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using si.ineor.webapi.Authorization;
using si.ineor.webapi.Entities;
using si.ineor.webapi.Models.Movie;
using si.ineor.webapi.Services;

namespace si.ineor.webapi.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: api/Movies
        [HttpGet("[action]")]
        public ActionResult<IEnumerable<Movie>> SearchAllMovies(string? SearchText = "")
        {
            return _movieService.GetAll(SearchText).ToList();
        }

        // GET: api/Movies/5
        [HttpGet("{id:guid}")]
        public ActionResult<Movie> GetMovie(Guid id)
        {
            var movie = _movieService.GetById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Role.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(Guid id, Movie movie)
        {
            var s = _movieService.UpdateMovie(id, movie);
            if(s != null)
            {
                return Ok(s);
            }
            return Problem("Entity set 'IneorwebapiContext.Movie'!");
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Role.Admin)]
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            //if (_context.Movie == null)
            //{
            //    return Problem("Entity set 'IneorwebapiContext.Movie'  is null.");
            //}
            var s = _movieService.AddMovie(movie);

            return CreatedAtAction("AddedMovie", s);
        }

        // DELETE: api/Movies/5
        [Authorize(Role.Admin)]
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(Guid id)
        {

            var movie = _movieService.GetById(id);
            if (movie == null)
            {
                return NotFound();
            }
            _movieService.DeleteMovie(movie);

            return Ok();
        }
    }
}
