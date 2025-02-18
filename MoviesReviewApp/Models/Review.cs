﻿namespace MoviesReviewApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public Reviewer Reviewer { get; set; }
        public Movie Movie { get; set; }
    }
}
