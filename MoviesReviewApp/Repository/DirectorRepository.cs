using AutoMapper;
using MoviesReviewApp.Data;
using MoviesReviewApp.Interfaces;
using MoviesReviewApp.Models;

namespace MoviesReviewApp.Repository
{
    public class DirectorRepository : IDirectorRepository
    { //where we put database calls
        private readonly DataContext _context;
        
        public DirectorRepository(DataContext context)
        {
            _context = context;
         
        }

        public bool CreateDirector(Director director)
        {
            _context.Add(director);
            return Save();
        }

        public bool DeleteDirector(Director director)
        {
            _context.Remove(director);
            return Save();
        }

        public bool DirectorExists(int dirId)
        {
            return _context.Directors.Any(d => d.Id == dirId);
        }

        public Director GetDirector(int dirId)
        {
            return _context.Directors.Where(d => d.Id == dirId).FirstOrDefault();    
        }

        public ICollection<Director> GetDirectorOfAMovie(int movieId)
        {
            return _context.MovieDirectors.Where(m => m.MovieId == movieId).Select(d => d.Director).ToList();
        }

        public ICollection<Director> GetDirectors()
        {
            return _context.Directors.ToList();
        }

        public ICollection<Movie> GetMovieByDirector(int dirId)
        {
            return _context.MovieDirectors.Where(d => d.Director.Id == dirId).Select(m => m.Movie).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDirector(Director director)
        {
            _context.Update(director);
            return Save();
        }
    }
}
