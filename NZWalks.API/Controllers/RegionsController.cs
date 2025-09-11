using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.API.CustomActionFilters;
using Microsoft.AspNetCore.Authorization;



namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController( IRegionRepository regionRepository , IMapper mapper)
        {
            this.regionRepository = regionRepository; 
            this.mapper = mapper;
        }


        //get all regions
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync(); 

            var regionsDTO = mapper.Map<List<RegionDTO>>(regionsDomain); 

            return Ok(regionsDTO); 
        }


        //get region by id
        [HttpGet]
        [Authorize(Roles = "Reader")]
        [Route("{id:guid}")]
        public async Task <IActionResult> GetByID([FromRoute]Guid id)
        {
            var region = await regionRepository.GetByIdAsync(id);   

            if (region == null)
            {
                return NotFound();
            }

            var regionsDTO = mapper.Map<RegionDTO>(region);

            


            return Ok(regionsDTO);
        }


        // create region
        [HttpPost]
        [Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task< IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
          
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDTO);

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel); // Check if the region already exists by ID

            // mapping domain model to DTO
            var createdRegionDTO = mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(GetByID), new { id = createdRegionDTO.Id }, createdRegionDTO);
          
        }


        //update region
        [HttpPut]
        [Authorize(Roles = "Writer")]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
           
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDTO);

                var updatedRegion = await regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                var updatedRegionDTO = mapper.Map<RegionDTO>(updatedRegion);

                return Ok(updatedRegionDTO);
        }


        //Delete Region
        [HttpDelete]
        [Authorize(Roles = "Writer")]
        [Route("{id:guid}")]
        public async Task < IActionResult > Delete([FromRoute] Guid id)
        {
            var deletedRegion = await regionRepository.DeleteAsync(id); 

            if (deletedRegion == null)
            {
                return NotFound(); 
            }

            var deletedRegionDTO = mapper.Map<RegionDTO>(deletedRegion); 

            return Ok(deletedRegionDTO);
        }
    }
}