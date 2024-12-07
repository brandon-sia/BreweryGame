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
        private readonly IQuoteDetailRepository _quoteDetailRepository;
        private readonly IQuoteRepository _quoteRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public QuoteDetailController(IQuoteDetailRepository quoteDetailRepository,
            IQuoteRepository quoteRepository,
            IInventoryRepository inventoryRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _quoteDetailRepository = quoteDetailRepository;
            _quoteRepository = quoteRepository;
            _inventoryRepository = inventoryRepository;

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<QuoteDetail>))]
        public IActionResult GetQuoteDetails()
        {
            var quotes = _mapper.Map<List<QuoteDetailDTO>>(_quoteDetailRepository.GetQuoteDetails());

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
            if (!_quoteDetailRepository.QuoteDetailExists(quoteDetailId))
            {
                return NotFound();
            }
            var quote = _mapper.Map<QuoteDetailDTO>(_quoteDetailRepository.GetQuoteDetail(quoteDetailId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(quote);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateQuoteDetail([FromBody] QuoteDetailDTO quoteDetailCreate)
        {
            if (quoteDetailCreate == null)
                return BadRequest(ModelState);

            var quote = _quoteRepository.GetQuote(quoteDetailCreate.QuoteId);

            if (quote == null)
            {
                ModelState.AddModelError("", "Quote does not exists");
                return StatusCode(422, ModelState);
            }

            var quoteDetail = _quoteDetailRepository.GetQuoteDetails()
                .Where(i => i.QuoteId == quoteDetailCreate.Id)
                .FirstOrDefault();

            if (quoteDetail != null)
            {
                ModelState.AddModelError("", "QuoteDetail already exists");
                return StatusCode(422, ModelState);
            }

      
            var inventoryQuantity = _inventoryRepository.GetInventories()
                .Where(i => i.WholesalerId == quote.WholesalerId && i.BeerId == quoteDetailCreate.BeerId)
                .FirstOrDefault().Stock;


            if (quoteDetailCreate.Quantity > inventoryQuantity)
            {
                ModelState.AddModelError("", "Insufficient stock for the beer quoted");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            var quoteMap = _mapper.Map<QuoteDetail>(quoteDetailCreate);

            if (!_quoteDetailRepository.CreateQuoteDetail(quoteMap))
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

            if (!_quoteDetailRepository.QuoteDetailExists(quoteDetailId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var quoteMap = _mapper.Map<QuoteDetail>(updatedQuoteDetail);

            if (!_quoteDetailRepository.UpdateQuoteDetail(quoteMap))
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
            if (!_quoteDetailRepository.QuoteDetailExists(quoteDetailId))
            {
                return NotFound();
            }

            var quoteToDelete = _quoteDetailRepository.GetQuoteDetail(quoteDetailId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_quoteDetailRepository.DeleteQuoteDetail(quoteToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting QuoteDetail");
            }

            return NoContent();
        }

    }
}
