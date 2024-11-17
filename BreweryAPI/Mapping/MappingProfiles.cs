using AutoMapper;
using BreweryAPI.DTO;
using BreweryAPI.Models;

namespace PokemonReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Beer, BeerDTO>().ReverseMap();
            CreateMap<Brewery, BreweryDTO>().ReverseMap();
            CreateMap<Wholesaler, WholesalerDTO>().ReverseMap();
            CreateMap<Inventory, InventoryDTO>().ReverseMap();
        }
    }
}
