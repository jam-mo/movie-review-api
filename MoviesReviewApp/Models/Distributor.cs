namespace MoviesReviewApp.Models
{
    public class Distributor
    {
        // country
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Director> Directors { get; set; }
    }
}
