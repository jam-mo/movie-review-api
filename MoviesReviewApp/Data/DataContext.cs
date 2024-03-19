using Microsoft.EntityFrameworkCore;
using MoviesReviewApp.Models;

namespace MoviesReviewApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        // 
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<MovieDirector> MovieDirectors { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGenre>()
                .HasKey(mc => new { mc.MovieId, mc.GenreId }); // tells ef to link these two id's together
            modelBuilder.Entity<MovieGenre>()
                .HasOne(m => m.Movie)
                .WithMany(mc => mc.MovieGenres)
                .HasForeignKey(m => m.MovieId);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(m => m.Genre)
                .WithMany(mc => mc.MovieGenres)
                .HasForeignKey(g => g.GenreId);

            modelBuilder.Entity<MovieDirector>()
                .HasKey(md => new { md.MovieId, md.DirectorId }); // tells ef to link these two id's together
            modelBuilder.Entity<MovieDirector>()
                .HasOne(m => m.Movie)
                .WithMany(md => md.MovieDirectors)
                .HasForeignKey(m => m.MovieId);

            modelBuilder.Entity<MovieDirector>()
                .HasOne(m => m.Director)
                .WithMany(md => md.MovieDirectors)
                .HasForeignKey(d => d.DirectorId);

        }

    }
}
