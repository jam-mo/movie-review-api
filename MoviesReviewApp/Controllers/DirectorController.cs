using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesReviewApp.Dto;
using MoviesReviewApp.Interfaces;
using MoviesReviewApp.Models;
using MoviesReviewApp.Repository;
using System.IO;

namespace MoviesReviewApp.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class DirectorController : Controller
    {
        private readonly IDirectorRepository _directorRepository;
        private readonly IDistributorRepository _distributorRepository;
        private readonly IMapper _mapper;
        public DirectorController(IDirectorRepository directorRepository, IDistributorRepository distributorRepository, IMapper mapper)
        {
            _directorRepository = directorRepository;
            _distributorRepository = distributorRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Director>))]
        public IActionResult GetDirectors()
        { // returning list of directors
            var directors = _mapper.Map<List<DirectorDto>>(_directorRepository.GetDirectors());
            // mapping the data
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(directors);
        }
        [HttpGet("{directorId}")]
        [ProducesResponseType(200, Type = typeof(Director))]
        [ProducesResponseType(400)]
        public IActionResult GetDirector(int directorId)
        // returning single distributor
        {
            if (!_directorRepository.DirectorExists(directorId))
                return NotFound();
            var director = _mapper.Map<DirectorDto>(_directorRepository.GetDirector(directorId));

            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(director);
        }
        [HttpGet("{directorId}/movie")]
        [ProducesResponseType(200, Type = typeof(Director))]
        [ProducesResponseType(400)]
        public IActionResult GetMovieByDirector(int directorId) 
        {
            if (!_directorRepository.DirectorExists(directorId))
                return NotFound();
            var movie = _mapper.Map<List<MovieDto>>(_directorRepository.GetMovieByDirector(directorId));

            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(movie);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDirector([FromQuery] int distributorId,[FromBody] DirectorDto directorCreate)
        {
            if (directorCreate == null)
                return BadRequest();
            var direct = _directorRepository.GetDirectors()
                .Where(d => d.LastName.Trim().ToUpper() == directorCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (direct != null)
            {
                ModelState.AddModelError("", "Director Already Exists");
                return StatusCode(422, ModelState);
                // 
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var directMap = _mapper.Map<Director>(directorCreate);

            directMap.Distributor = _distributorRepository.GetDistributor(distributorId);

            if (!_directorRepository.CreateDirector(directMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }
        [HttpPut("{directorId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDirector(int directorId, [FromBody] DirectorDto updatedDirector)
        {
            if (updatedDirector == null)
                return BadRequest(ModelState);
            if (directorId != updatedDirector.Id)
                return BadRequest(ModelState);
            if (!_directorRepository.DirectorExists(directorId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            var directorMap = _mapper.Map<Director>(updatedDirector);
            if (!_directorRepository.UpdateDirector(directorMap))
            {
                ModelState.AddModelError("", "Something went wrong updating the distributor");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{directorId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDirector(int directorId)
        {
            if (!_directorRepository.DirectorExists(directorId))
            {
                return NotFound();
            }
            var directorToRemove = _directorRepository.GetDirector(directorId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (_directorRepository.DeleteDirector(directorToRemove))
            {
                ModelState.AddModelError("", "Something went wrong deleting the director");
            }
            return NoContent();

        }
    }
}
