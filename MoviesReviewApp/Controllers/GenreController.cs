using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesReviewApp.Dto;
using MoviesReviewApp.Interfaces;
using MoviesReviewApp.Models;
using MoviesReviewApp.Repository;

namespace MoviesReviewApp.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class GenreController : Controller
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        public GenreController(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
        public IActionResult GetGenres()
        { // returning list of genres
            var genres = _mapper.Map<List<GenreDto>>(_genreRepository.GetGenres());
            // mapping the data
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(genres);
        }
        [HttpGet("{genreId}")]
        [ProducesResponseType(200, Type = typeof(Genre))]
        [ProducesResponseType(400)]
        public IActionResult GetGenre(int genreId)
        // returning single genre
        {
            if (!_genreRepository.GenreExists(genreId))
                return NotFound();
            var genre = _mapper.Map<GenreDto>(_genreRepository.GetGenre(genreId));

            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(genre);
        }
        [HttpGet("movie/{genreId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
        [ProducesResponseType(400)]
        public IActionResult GetMovieByGenre(int genreId) 
        {
            var movies = _mapper.Map<List<MovieDto>>(_genreRepository.GetMovieByGenre(genreId));

            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(movies);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGenre([FromBody] GenreDto genreCreate)
        {
            if (genreCreate ==  null)
                return BadRequest();
            var genre = _genreRepository.GetGenres()
                .Where(g => g.Name.Trim().ToUpper() == genreCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (genre != null)
            {
                ModelState.AddModelError("", "Genre Already Exists");
                return StatusCode(422, ModelState);
                // handles error if genre already exists
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var genreMap = _mapper.Map<Genre>(genreCreate);

            if (!_genreRepository.CreateGenre(genreMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }
        [HttpPut("{genreId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateGenre(int genreId, [FromBody] GenreDto updatedGenre) 
        {
            if (updatedGenre == null)
                return BadRequest(ModelState);
            if (genreId != updatedGenre.Id)
                return BadRequest(ModelState);
            if (!_genreRepository.GenreExists(genreId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            var genreMap = _mapper.Map<Genre>(updatedGenre);
            if (!_genreRepository.UpdateGenre(genreMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the genre");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{genreId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteGenre(int genreId)
        {
            if (!_genreRepository.GenreExists(genreId))
            {
                return NotFound();
            }
            var genreToRemove = _genreRepository.GetGenre(genreId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (_genreRepository.DeleteGenre(genreToRemove))
            {
                ModelState.AddModelError("", "Something went wrong deleting the genre");
            }
            return NoContent();
            
        }
    }
}
