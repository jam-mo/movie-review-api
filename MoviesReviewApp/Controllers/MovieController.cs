using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MoviesReviewApp.Dto;
using MoviesReviewApp.Interfaces;
using MoviesReviewApp.Models;

namespace MoviesReviewApp.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieController(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
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

    }
}

