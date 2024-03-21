using MoviesReviewApp.Models;
using System.ComponentModel;

namespace MoviesReviewApp.Interfaces
{
    public interface IGenreRepository
    {
        ICollection<Genre> GetGenres();
        Genre GetGenre(int id);
        ICollection<Movie> GetMovieByGenre(int genreID);
        bool GenreExists(int id);
        //
        bool CreateGenre(Genre genre);
        bool UpdateGenre(Genre genre);
        bool DeleteGenre(Genre genre);
        bool Save();
    }
}
