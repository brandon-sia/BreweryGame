using AutoMapper;
using BreweryAPI.DTO;
using BreweryAPI.Interfaces;
using BreweryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WholesalerController : Controller
    {
        private readonly IWholesalerRepository _wholesalerRepository;
        private readonly IMapper _mapper;

        public WholesalerController(IWholesalerRepository wholesalerRepository, IMapper mapper)
        {
            _mapper = mapper;
            _wholesalerRepository = wholesalerRepository;
             
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Wholesaler>))]
        public IActionResult GetWholeSaler()
        {
            var breweries = _mapper.Map<List<WholesalerDTO>>(_wholesalerRepository.GetWholesalers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(breweries);
        }

        [HttpGet("{wholesalerId}")]
        [ProducesResponseType(200, Type = typeof(Wholesaler))]
        [ProducesResponseType(400)]
        public IActionResult GetBrewery(int wholesalerId)
        {
            if (!_wholesalerRepository.WholesalerExists(wholesalerId))
            {
                return NotFound();
            }
            var wholesaler = _mapper.Map<WholesalerDTO>(_wholesalerRepository.GetWholesaler(wholesalerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(wholesaler);
        }

        //[HttpGet("beers/{wholesalerId}")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Beer>))]
        //[ProducesResponseType(400)]

        //public IActionResult GetBeerByBreweryId(int wholesalerId)
        //{
        //    var beers = _mapper.Map<List<BeerDTO>>(_wholesalerRepository.GetBeerByWholesaler(wholesalerId));

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(beers);
        //}

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateWholesaler([FromBody] WholesalerDTO wholesalerCreate)
        {
            if (wholesalerCreate == null)
                return BadRequest(ModelState);

            var wholesaler = _wholesalerRepository.GetWholesalers()
                .Where(c => c.Name.Trim().ToUpper() == wholesalerCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (wholesaler != null)
            {
                ModelState.AddModelError("", "Wholesaler already exists");
                return StatusCode(422, ModelState);
            }


            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            var wholesalerMap = _mapper.Map<Wholesaler>(wholesalerCreate);

            if (!_wholesalerRepository.CreateWholesaler(wholesalerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }


        [HttpPut("{wholesalerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBrewery(int wholesalerId, [FromBody] WholesalerDTO updatedWholesaler)
        {
            if (updatedWholesaler == null)
            {
                return BadRequest(ModelState);
            }

            if (wholesalerId != updatedWholesaler.Id)
                return BadRequest(ModelState);

            if (!_wholesalerRepository.WholesalerExists(wholesalerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var wholesalerMap = _mapper.Map<Wholesaler>(updatedWholesaler);

            if (!_wholesalerRepository.UpdateWholesaler(wholesalerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating wholesaler");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{wholesalerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteBrewery(int wholesalerId)
        {
            if (!_wholesalerRepository.WholesalerExists(wholesalerId))
            {
                return NotFound();
            }

            var wholesalerToDelete = _wholesalerRepository.GetWholesaler(wholesalerId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_wholesalerRepository.DeleteWholesaler(wholesalerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting wholesaler");
            }

            return NoContent();
        }

    }
}
