using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MoviesReviewApp.Dto;
using MoviesReviewApp.Interfaces;
using MoviesReviewApp.Models;
using MoviesReviewApp.Repository;

namespace MoviesReviewApp.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IDirectorRepository _directorRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public MovieController(IMovieRepository movieRepository, IMapper mapper, IDirectorRepository directorRepository, IReviewRepository reviewRepository)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _directorRepository = directorRepository;
            _reviewRepository = reviewRepository;
        }
        [HttpGet]
        [ProducesResponseType(200,Type= typeof(IEnumerable<Movie>))]
        public IActionResult GetMovies()
        { // returning list of movies
            var movies = _mapper.Map<List<MovieDto>>(_movieRepository.GetMovies());
            // mapping the data
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(movies);
        }

        [HttpGet("{movieId}")]
        [ProducesResponseType(200, Type= typeof(Movie))]
        [ProducesResponseType(400)]
        public IActionResult GetMovie(int movieId)
        // returning single movie
        {
            if (!_movieRepository.MovieExists(movieId))
                return NotFound();
            var movie = _mapper.Map<MovieDto>(_movieRepository.GetMovie(movieId));

            if (!ModelState.IsValid) 
                return BadRequest();
            return Ok(movie);
        }
        [HttpGet("{movieId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetMovieRating(int movieId)
        {
            if (_movieRepository.MovieExists(movieId))
                return NotFound();
            var movie = _movieRepository.GetMovieRating(movieId);

            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(movie);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMovie([FromQuery] int directorId, [FromQuery] int genreId,  [FromBody] MovieDto movieCreate)
        {
            if (movieCreate == null)
                return BadRequest();
            var movie = _movieRepository.GetMovies()
                .Where(m => m.Title.Trim().ToUpper() == movieCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (movie != null)
            {
                ModelState.AddModelError("", "Movie Already Exists");
                return StatusCode(422, ModelState);
                // handles error if genre already exists
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var movieMap = _mapper.Map<Movie>(movieCreate);

            

            if (!_movieRepository.CreateMovie(directorId, genreId, movieMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }
        [HttpPut("{movieId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateMovie([FromQuery] int directorId, [FromQuery] int genreId, int movieId, [FromBody] MovieDto updatedMovie)
        {
            if (updatedMovie == null)
                return BadRequest(ModelState);
            if (movieId != updatedMovie.Id)
                return BadRequest(ModelState);
            if (!_movieRepository.MovieExists(movieId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            var movieMap = _mapper.Map<Movie>(updatedMovie);
            if (!_movieRepository.UpdateMovie(directorId, genreId,movieMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the distributor");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{movieId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteMovie(int movieId)
        {
            if (!_movieRepository.MovieExists(movieId))
            {
                return NotFound();
            }var reviewsToDelete = _reviewRepository.GetReviewsOfAMovie(movieId);
            var movieToRemove = _movieRepository.GetMovie(movieId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting reviews");
            }
            if (_movieRepository.DeleteMovie(movieToRemove))
            {
                ModelState.AddModelError("", "Something went wrong deleting the movie");
            }
            return NoContent();

        }


    }
}

