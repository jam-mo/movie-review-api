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

        public bool CreateMovie(int directorId, int genreId, Movie movie)
        {
            var directorEntity = _context.Directors.Where(d => d.Id == directorId).FirstOrDefault();
            var genreEntity = _context.Genres.Where(g =>  g.Id == genreId).FirstOrDefault();

            var movieDirector = new MovieDirector()
            {
                Director = directorEntity,
                Movie = movie,
            };
            _context.Add(movieDirector);
            var movieGenre = new MovieGenre()
            {
                Genre = genreEntity,
                Movie = movie,
            };
            _context.Add(movieGenre);

            _context.Add(movie);
            return Save();
        }

        public bool DeleteMovie(Movie movie)
        {
            _context.Remove(movie);
            return Save();
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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateMovie(int directorId, int genreId, Movie movie)
        {
            _context.Update(movie);
            return Save();
        }
    }
}
