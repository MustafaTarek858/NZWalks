using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using AutoMapper;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            // Create a mapping configuration for AutoMapper
            CreateMap<Region, RegionDTO>().ReverseMap(); // Allows mapping in both directions
            CreateMap<AddRegionRequestDTO , Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDTO, Region>().ReverseMap();
            CreateMap<Walk, AddWalkRequestDTO>().ReverseMap();
            CreateMap<Walk, walkDTO>().ReverseMap();
            CreateMap<UpdateWalkRequestDTO, Walk>().ReverseMap();

        }
    }
}
