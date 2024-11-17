using AutoMapper;
using BreweryAPI.DTO;
using BreweryAPI.Interfaces;
using BreweryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreweryController : Controller
    {
        private readonly IBreweryRepository _breweryRepository;
        private readonly IMapper _mapper;

        public BreweryController(IBreweryRepository breweryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _breweryRepository = breweryRepository;
             
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Brewery>))]
        public IActionResult GetBrewery()
        {
            var breweries = _mapper.Map<List<BreweryDTO>>(_breweryRepository.GetBreweries());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(breweries);
        }

        [HttpGet("{breweryId}")]
        [ProducesResponseType(200, Type = typeof(Brewery))]
        [ProducesResponseType(400)]
        public IActionResult GetBrewery(int breweryId)
        {
            if (!_breweryRepository.BreweryExists(breweryId))
            {
                return NotFound();
            }
            var brewery = _mapper.Map<BreweryDTO>(_breweryRepository.GetBrewery(breweryId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(brewery);
        }

        [HttpGet("beers/{breweryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Beer>))]
        [ProducesResponseType(400)]

        public IActionResult GetBeerByBreweryId(int breweryId)
        {
            var beers = _mapper.Map<List<BeerDTO>>(_breweryRepository.GetBeerByBrewery(breweryId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(beers);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBrewery([FromBody] BreweryDTO breweryCreate)
        {
            if (breweryCreate == null)
                return BadRequest(ModelState);

            var brewery = _breweryRepository.GetBreweries()
                .Where(c => c.Name.Trim().ToUpper() == breweryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (brewery != null)
            {
                ModelState.AddModelError("", "Brewery already exists");
                return StatusCode(422, ModelState);
            }


            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            var breweryMap = _mapper.Map<Brewery>(breweryCreate);

            if (!_breweryRepository.CreateBrewery(breweryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }

        [HttpPut("{breweryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBrewery(int breweryId, [FromBody] BreweryDTO updatedBrewery)
        {
            if (updatedBrewery == null)
            {
                return BadRequest(ModelState);
            }

            if (breweryId != updatedBrewery.Id)
                return BadRequest(ModelState);

            if (!_breweryRepository.BreweryExists(breweryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var breweryMap = _mapper.Map<Brewery>(updatedBrewery);

            if (!_breweryRepository.UpdateBrewery(breweryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating brewery");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{breweryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteBrewery(int breweryId)
        {
            if (!_breweryRepository.BreweryExists(breweryId))
            {
                return NotFound();
            }

            var breweryToDelete = _breweryRepository.GetBrewery(breweryId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_breweryRepository.DeleteBrewery(breweryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting brewery");
            }

            return NoContent();
        }



    }
}
