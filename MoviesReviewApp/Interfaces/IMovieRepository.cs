using MoviesReviewApp.Models;

namespace MoviesReviewApp.Interfaces
{
    public interface IMovieRepository
    {
        ICollection<Movie> GetMovies();
        Movie GetMovie(int id);
        Movie GetMovie(string name);
        decimal GetMovieRating(int movieId);
        bool MovieExists(int movieId);
    }
    
}
