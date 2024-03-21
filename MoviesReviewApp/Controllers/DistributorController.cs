using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesReviewApp.Dto;
using MoviesReviewApp.Interfaces;
using MoviesReviewApp.Models;
using MoviesReviewApp.Repository;

namespace MoviesReviewApp.Controllers;

    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class DistributorController : Controller
    {
    private readonly IDistributorRepository _distributorRepository;
    private readonly IMapper _mapper;
    public DistributorController(IDistributorRepository distributorRepository, IMapper mapper)
    {
        _distributorRepository = distributorRepository;
        _mapper = mapper;
    }
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Distributor>))]
    public IActionResult GetDistributors()
    { // returning list of distributors
        var distributors = _mapper.Map<List<DistributorDto>>(_distributorRepository.GetDistributors());
        // mapping the data
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(distributors);
    }
    [HttpGet("{distributorId}")]
    [ProducesResponseType(200, Type = typeof(Distributor))]
    [ProducesResponseType(400)]
    public IActionResult GetDistributor(int distributorId)
    // returning single distributor
    {
        if (!_distributorRepository.DistributorExists(distributorId))
            return NotFound();
        var dist = _mapper.Map<DistributorDto>(_distributorRepository.GetDistributor(distributorId));

        if (!ModelState.IsValid)
            return BadRequest();
        return Ok(dist);
    }
    [HttpGet("/directors/{directorId}")]
    [ProducesResponseType(200, Type = typeof(Distributor))]
    [ProducesResponseType(400)]
    public IActionResult GetDistributorOfDirector(int directorId)
    {
        var dist = _mapper.Map<DistributorDto>(_distributorRepository.GetDistributorByDirector(directorId));

        if (!ModelState.IsValid)
            return BadRequest();
        return Ok(dist);
    }
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateDistributor([FromBody] DistributorDto distributorCreate)
    {
        if (distributorCreate == null)
            return BadRequest();
        var dist = _distributorRepository.GetDistributors()
            .Where(d => d.Name.Trim().ToUpper() == distributorCreate.Name.TrimEnd().ToUpper())
            .FirstOrDefault();
        if (dist != null)
        {
            ModelState.AddModelError("", "Distributor Already Exists");
            return StatusCode(422, ModelState);
            // handles error if genre already exists
        }
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var distMap = _mapper.Map<Distributor>(distributorCreate);

        if (!_distributorRepository.CreateDistributor(distMap))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfully created");
    }
    [HttpPut("{distributorId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult UpdateDistributor(int distributorId, [FromBody] DistributorDto updatedDistributor)
    {
        if (updatedDistributor == null)
            return BadRequest(ModelState);
        if (distributorId != updatedDistributor.Id)
            return BadRequest(ModelState);
        if (!_distributorRepository.DistributorExists(distributorId))
            return NotFound();
        if (!ModelState.IsValid)
            return BadRequest();

        var distributorMap = _mapper.Map<Distributor>(updatedDistributor);
        if (!_distributorRepository.UpdateDistributor(distributorMap))
        {
            ModelState.AddModelError("", "Something went wrong updating the distributor");
            return StatusCode(500, ModelState);
        }
        return NoContent();
    }
    [HttpDelete("{distributorId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult DeleteDistributor(int distributorId)
    {
        if (!_distributorRepository.DistributorExists(distributorId))
        {
            return NotFound();
        }
        var distToRemove = _distributorRepository.GetDistributor(distributorId);
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (_distributorRepository.DeleteDistributor(distToRemove))
        {
            ModelState.AddModelError("", "Something went wrong deleting the distributor");
        }
        return NoContent();

    }

}

