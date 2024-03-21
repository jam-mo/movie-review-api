using MoviesReviewApp.Data;
using MoviesReviewApp.Interfaces;
using MoviesReviewApp.Models;

namespace MoviesReviewApp.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _context;
        public GenreRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateGenre(Genre genre)
        {
           
            _context.Add(genre);
           
            return Save();
        }

        public bool DeleteGenre(Genre genre)
        {
            _context.Remove(genre);
            return Save();
        }

        public bool GenreExists(int id)
        {
            return _context.Genres.Any(G => G.Id == id);
            // RETrieves genre from db that matches the id, and returns true if exists in db
        }

        public Genre GetGenre(int id)
        {
            return _context.Genres.Where(g => g.Id == id).FirstOrDefault();
            // retrieves genre based off id,
        }

        public ICollection<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }

        public ICollection<Movie> GetMovieByGenre(int genreID)
        {
            return _context.MovieGenres.Where(g => g.GenreId == genreID).Select(m => m.Movie).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateGenre(Genre genre)
        {
            _context.Update(genre);
            return Save();
        }
    }
}
