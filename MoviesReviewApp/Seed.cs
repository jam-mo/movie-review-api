using MoviesReviewApp.Data;
using MoviesReviewApp.Models;

namespace MoviesReviewApp
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext dC)
        {
            this.dataContext = dC;
        }
        public void SeedDataContext()
        {
            if (!dataContext.MovieDirectors.Any())
            {
                var movieDirectors = new List<MovieDirector>()
                {
                    new MovieDirector()
                    {
                        Movie = new Movie()
                        {
                            Title = "The Prestige",
                            ReleaseYear = 2006,
                            MovieGenres = new List<MovieGenre>()
                            {
                                new MovieGenre { Genre = new Genre() {Name = "Thriller"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="The Prestige", Description="Christopher Nolans Best work", Rating=5,
                                Reviewer=new Reviewer(){FirstName = "Jonathan", LastName="Jacoba"} },
                                new Review { Title="The Prestige", Description="Great work of jackman and bale", Rating=5,
                                Reviewer=new Reviewer(){FirstName = "Jamus", LastName="McSeamus"} },
                                new Review { Title="The Prestige", Description="Superb Thriller", Rating=4,
                                Reviewer=new Reviewer(){FirstName = "Roger", LastName="Norville"} },
                            }
                        },
                        Director = new Director()
                        {
                            FirstName = "Christopher",
                            LastName = "Nolan",
                            Distributor = new Distributor()
                            {
                                Name = "Warner Bros Pictures"
                            }
                        }
                    }, // insert new here
                    new MovieDirector()
                    {
                        Movie = new Movie()
                        {
                            Title = "Big Daddy",
                            ReleaseYear = 1999,
                            MovieGenres = new List<MovieGenre>()
                            {
                                new MovieGenre { Genre = new Genre() {Name = "Comedy"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Big Daddy", Description="Heart warming and funny", Rating=3,
                                Reviewer=new Reviewer(){FirstName = "David", LastName="Jacobs"} },
                                new Review { Title="Big Daddy", Description="Supreme Sandler", Rating=5,
                                Reviewer=new Reviewer(){FirstName = "Chris", LastName="Banks"} },
                                new Review { Title="Big Daddy", Description="One for the ages", Rating=4,
                                Reviewer=new Reviewer(){FirstName = "Melvin", LastName="Doo"} },
                            }
                        },
                        Director = new Director()
                        {
                            FirstName = "Dennis",
                            LastName = "Dugan",
                            Distributor = new Distributor()
                            {
                                Name = "Sony Pictures"
                            }
                        }
                    },
                };
                dataContext.MovieDirectors.AddRange(movieDirectors);
                dataContext.SaveChanges();
            }
        }
    }
}
