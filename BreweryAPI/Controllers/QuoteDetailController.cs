using AutoMapper;
using BreweryAPI.DTO;
using BreweryAPI.Interfaces;
using BreweryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteDetailController : Controller
    {
        private readonly IQuoteDetailRepository _quoteRepository;
        private readonly IMapper _mapper;

        public QuoteDetailController(IQuoteDetailRepository quoteRepository, IMapper mapper)
        {
            _mapper = mapper;
            _quoteRepository = quoteRepository;
             
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<QuoteDetail>))]
        public IActionResult GetQuoteDetails()
        {
            var quotes = _mapper.Map<List<QuoteDetailDTO>>(_quoteRepository.GetQuoteDetails());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(quotes);
        }

        [HttpGet("{quoteDetailId}")]
        [ProducesResponseType(200, Type = typeof(QuoteDetail))]
        [ProducesResponseType(400)]
        public IActionResult GetQuoteDetail(int quoteDetailId)
        {
            if (!_quoteRepository.QuoteDetailExists(quoteDetailId))
            {
                return NotFound();
            }
            var quote = _mapper.Map<QuoteDetailDTO>(_quoteRepository.GetQuoteDetail(quoteDetailId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(quote);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateQuoteDetail([FromBody] QuoteDetailDTO quoteCreate)
        {
            if (quoteCreate == null)
                return BadRequest(ModelState);

            var quote = _quoteRepository.GetQuoteDetails()
                .Where(i => i.Id == quoteCreate.Id)
                .FirstOrDefault();

            if (quote != null)
            {
                ModelState.AddModelError("", "QuoteDetail already exists");
                return StatusCode(422, ModelState);
            }


            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            var quoteMap = _mapper.Map<QuoteDetail>(quoteCreate);

            if (!_quoteRepository.CreateQuoteDetail(quoteMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }


        [HttpPut("{quoteDetailId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateQuoteDetail(int quoteDetailId, [FromBody] QuoteDetailDTO updatedQuoteDetail)
        {
            if (updatedQuoteDetail == null)
            {
                return BadRequest(ModelState);
            }

            if (quoteDetailId != updatedQuoteDetail.Id)
                return BadRequest(ModelState);

            if (!_quoteRepository.QuoteDetailExists(quoteDetailId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var quoteMap = _mapper.Map<QuoteDetail>(updatedQuoteDetail);

            if (!_quoteRepository.UpdateQuoteDetail(quoteMap))
            {
                ModelState.AddModelError("", "Something went wrong updating QuoteDetail");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{quoteDetailId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteQuoteDetail(int quoteDetailId)
        {
            if (!_quoteRepository.QuoteDetailExists(quoteDetailId))
            {
                return NotFound();
            }

            var quoteToDelete = _quoteRepository.GetQuoteDetail(quoteDetailId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_quoteRepository.DeleteQuoteDetail(quoteToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting QuoteDetail");
            }

            return NoContent();
        }

    }
}
