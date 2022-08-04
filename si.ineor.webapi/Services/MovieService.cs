using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using si.ineor.webapi.Entities;
using si.ineor.webapi.Helpers;
using si.ineor.webapi.Models.Movie;

namespace si.ineor.webapi.Services
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetAll(string SearchText);
        Movie GetById(Guid id);
        Movie AddMovie(Movie movie);
        Movie UpdateMovie(Guid id, Movie movie);
        void DeleteMovie(Movie movie);
    }
    public class MovieService : IMovieService
    {
        private IneorwebapiContext _context;

        public MovieService(IneorwebapiContext context) {
            _context = context;
        }

        public Movie AddMovie(Movie movie)
        {

           var saved = _context.Movie.Add(movie);
            _context.SaveChanges();

            return saved.Entity;
        }

        public void DeleteMovie(Movie movie)
        {
            _context.Movie.Remove(movie);
            _context.SaveChanges();
        }

        public IEnumerable<Movie> GetAll(string SearchText)
        {
            if (SearchText != "")
            {
                return _context.Movie.Where(movie => movie.Title.Contains(SearchText));
            }
            else
            {
                return _context.Movie;
            }
        }

        public Movie GetById(Guid id)
        {
            var movie = _context.Movie.FirstOrDefault(x=>x.Id == id);
            if(movie == null)
            {
                throw new AppException("Movie not found");
            }
            return movie;
        }

        public Movie UpdateMovie(Guid id, Movie movie)
        {
            if(id != movie.Id)
            {
                throw new AppException("Missmatched ID");
            }
            _context.Entry(movie).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new AppException("Error saving movie");

            }

            return MovieExists(id);
        }
        private Movie MovieExists(Guid id)
        {
            return _context.Movie?.FirstOrDefault(e => e.Id == id);
        }
    }
}
