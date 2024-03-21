namespace MoviesReviewApp.Models
{
    public class Director
    {
        
        public int Id { get; set; }
        public  string FirstName { get; set; }
        public string LastName { get; set; }
        public Distributor Distributor { get; set; }
        public ICollection<MovieDirector> MovieDirectors { get; set; }   

    }
}
