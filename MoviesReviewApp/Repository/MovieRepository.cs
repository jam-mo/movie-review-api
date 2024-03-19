using MoviesReviewApp.Data;
using MoviesReviewApp.Interfaces;
using MoviesReviewApp.Models;

namespace MoviesReviewApp.Repository
{
    public class MovieRepository : IMovieRepository
    {
        // way to get our database calls
        private readonly DataContext _context;
        public MovieRepository(DataContext context)
        {
            _context = context;
        }

        public Movie GetMovie(int id)
        {
            // goes into entity, and shows the first entity it finds that matches
            return _context.Movies.Where(m => m.Id == id).FirstOrDefault();
        }

        public Movie GetMovie(string title)
        {
            return _context.Movies.Where(m => m.Title == title).FirstOrDefault();
        }

        public decimal GetMovieRating(int movieId)
        {
            var review = _context.Reviews.Where(m => m.Movie.Id == movieId);
            if (review.Count() <= 0) // if no reviews, return 0 for rating
                return 0;
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        // cant be edited, only shown
        public ICollection<Movie> GetMovies()
        {

            return _context.Movies.OrderBy(m => m.Id).ToList();
        }

        public bool MovieExists(int movieId)
        {
            return _context.Movies.Any(m => m.Id == movieId); 
        }
    }
}
