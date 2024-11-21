using AutoMapper;
using BreweryAPI.DTO;
using BreweryAPI.Interfaces;
using BreweryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : Controller
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IMapper _mapper;

        public QuoteController(IQuoteRepository quoteRepository, IMapper mapper)
        {
            _mapper = mapper;
            _quoteRepository = quoteRepository;
             
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Quote>))]
        public IActionResult GetQuotes()
        {
            var quotes = _mapper.Map<List<QuoteDTO>>(_quoteRepository.GetQuotes());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(quotes);
        }

        [HttpGet("{quoteId}")]
        [ProducesResponseType(200, Type = typeof(Quote))]
        [ProducesResponseType(400)]
        public IActionResult GetQuote(int quoteId)
        {
            if (!_quoteRepository.QuoteExists(quoteId))
            {
                return NotFound();
            }
            var quote = _mapper.Map<QuoteDTO>(_quoteRepository.GetQuote(quoteId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(quote);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateQuote([FromBody] QuoteDTO quoteCreate)
        {
            if (quoteCreate == null)
                return BadRequest(ModelState);

            var quote = _quoteRepository.GetQuotes()
                .Where(i => i.Id == quoteCreate.Id)
                .FirstOrDefault();

            if (quote != null)
            {
                ModelState.AddModelError("", "Quote already exists");
                return StatusCode(422, ModelState);
            }


            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            var quoteMap = _mapper.Map<Quote>(quoteCreate);

            if (!_quoteRepository.CreateQuote(quoteMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }


        [HttpPut("{quoteId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateQuote(int quoteId, [FromBody] QuoteDTO updatedQuote)
        {
            if (updatedQuote == null)
            {
                return BadRequest(ModelState);
            }

            if (quoteId != updatedQuote.Id)
                return BadRequest(ModelState);

            if (!_quoteRepository.QuoteExists(quoteId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var quoteMap = _mapper.Map<Quote>(updatedQuote);

            if (!_quoteRepository.UpdateQuote(quoteMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Quote");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{quoteId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteQuote(int quoteId)
        {
            if (!_quoteRepository.QuoteExists(quoteId))
            {
                return NotFound();
            }

            var quoteToDelete = _quoteRepository.GetQuote(quoteId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_quoteRepository.DeleteQuote(quoteToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Quote");
            }

            return NoContent();
        }

    }
}
