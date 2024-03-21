using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesReviewApp.Models;

namespace MoviesReviewApp.Interfaces
{
    public interface IDirectorRepository
    {
        ICollection<Director> GetDirectors(); //editable version of IEnumerable
        Director GetDirector(int dirId);
        ICollection<Director>GetDirectorOfAMovie(int movieId);
        ICollection<Movie> GetMovieByDirector(int dirId);
        bool DirectorExists(int dirId);
        bool CreateDirector(Director director);
        bool UpdateDirector(Director director);
        bool DeleteDirector(Director director);
        bool Save();
    }
}
