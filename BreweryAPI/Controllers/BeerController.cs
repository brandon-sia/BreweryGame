using AutoMapper;
using BreweryAPI.DTO;
using BreweryAPI.Interfaces;
using BreweryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : Controller
    {
        private readonly IBeerRepository _beerRepository;
        private readonly IBreweryRepository _breweryRepository;
        private readonly IMapper _mapper;

        public BeerController(IBeerRepository beerRepository, IBreweryRepository breweryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _beerRepository = beerRepository;
            _breweryRepository = breweryRepository;
             
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Beer>))]
        public IActionResult GetBeers()
        {
            var beers = _mapper.Map<List<BeerDTO>>(_beerRepository.GetBeers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(beers);
        }

        [HttpGet("{beerId}")]
        [ProducesResponseType(200, Type = typeof(Beer))]
        [ProducesResponseType(400)]
        public IActionResult GetBeer(int beerId)
        {
            if (!_beerRepository.BeerExists(beerId))
            {
                return NotFound();
            }
            var category = _mapper.Map<List<BeerDTO>>(_beerRepository.GetBeer(beerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }

        [HttpGet("brewery/{beerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Beer>))]
        [ProducesResponseType(400)]

        public IActionResult GetBreweryBybeerId(int beerId)
        {
            var breweries = _mapper.Map<List<BreweryDTO>>(_beerRepository.GetBreweryByBeer(beerId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(breweries);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBeer([FromQuery] int brewerId, [FromBody] BeerDTO beerCreate)
        {
            if (beerCreate == null)
                return BadRequest(ModelState);

            var beer = _beerRepository.GetBeers()
                .Where(c => c.Name.Trim().ToUpper() == beerCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (beer != null)
            {
                ModelState.AddModelError("", "Beer already exists");
                return StatusCode(422, ModelState);
            }


            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            var beerMap = _mapper.Map<Beer>(beerCreate);
            beerMap.BrewerId = brewerId;
            beerMap.Brewer = _breweryRepository.GetBrewery(brewerId);

            if (!_beerRepository.CreateBeer(beerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }

        [HttpPut("{beerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBeer(int beerId, [FromBody] BeerDTO updatedBeer)
        {
            if (updatedBeer == null)
            {
                return BadRequest(ModelState);
            }

            if (beerId != updatedBeer.Id)
                return BadRequest(ModelState);

            if (!_beerRepository.BeerExists(beerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var beerMap = _mapper.Map<Beer>(updatedBeer);

            if (!_beerRepository.UpdateBeer(beerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating beer");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{beerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteCategory(int beerId)
        {
            if (!_beerRepository.BeerExists(beerId))
            {
                return NotFound();
            }

            var beerToDelete = _beerRepository.GetBeer(beerId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_beerRepository.DeleteBeer(beerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting beer");
            }

            return NoContent();
        }



    }
}
