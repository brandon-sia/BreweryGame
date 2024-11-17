using AutoMapper;
using BreweryAPI.DTO;
using BreweryAPI.Interfaces;
using BreweryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : Controller
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public InventoryController(IInventoryRepository inventoryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _inventoryRepository = inventoryRepository;
             
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Inventory>))]
        public IActionResult GetInventories()
        {
            var inventories = _mapper.Map<List<InventoryDTO>>(_inventoryRepository.GetInventories());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(inventories);
        }

        [HttpGet("{inventoryId}")]
        [ProducesResponseType(200, Type = typeof(Inventory))]
        [ProducesResponseType(400)]
        public IActionResult GetInventory(int inventoryId)
        {
            if (!_inventoryRepository.InventoryExists(inventoryId))
            {
                return NotFound();
            }
            var inventory = _mapper.Map<InventoryDTO>(_inventoryRepository.GetInventory(inventoryId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(inventory);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateInventory([FromBody] InventoryDTO inventoryCreate)
        {
            if (inventoryCreate == null)
                return BadRequest(ModelState);

            var inventory = _inventoryRepository.GetInventories()
                .Where(i => i.BeerId == inventoryCreate.BeerId && i.WholesalerId == inventoryCreate.WholesalerId)
                .FirstOrDefault();

            if (inventory != null)
            {
                ModelState.AddModelError("", "Inventory already exists");
                return StatusCode(422, ModelState);
            }


            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            var inventoryMap = _mapper.Map<Inventory>(inventoryCreate);

            if (!_inventoryRepository.CreateInventory(inventoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }


        [HttpPut("{inventoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateInventory(int inventoryId, [FromBody] InventoryDTO updatedInventory)
        {
            if (updatedInventory == null)
            {
                return BadRequest(ModelState);
            }

            if (inventoryId != updatedInventory.Id)
                return BadRequest(ModelState);

            if (!_inventoryRepository.InventoryExists(inventoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var inventoryMap = _mapper.Map<Inventory>(updatedInventory);

            if (!_inventoryRepository.UpdateInventory(inventoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Inventory");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{inventoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteBrewery(int inventoryId)
        {
            if (!_inventoryRepository.InventoryExists(inventoryId))
            {
                return NotFound();
            }

            var inventoryToDelete = _inventoryRepository.GetInventory(inventoryId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_inventoryRepository.DeleteInventory(inventoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Inventory");
            }

            return NoContent();
        }

    }
}
