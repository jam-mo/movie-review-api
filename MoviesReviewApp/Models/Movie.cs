namespace MoviesReviewApp.Models
{
    public class Movie
    {
        
        public int Id { get; set; }
        public string Title { get; set; }
        
        public int ReleaseYear { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<MovieDirector> MovieDirectors { get; set; }
        public ICollection<MovieGenre>  MovieGenres { get; set; }

    }
}
