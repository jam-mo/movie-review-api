namespace MoviesReviewApp.Models
{
    public class Distributor
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Director> Directors { get; set; }
    }
}
