namespace MoviesReviewApp.Models
{
    public class Genre
    {
        // category
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set;}
    }
}
