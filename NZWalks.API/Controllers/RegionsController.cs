using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Diagnostics.Eventing.Reader;

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
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync(); 

            var regionsDTO = mapper.Map<List<RegionDTO>>(regionsDomain); 

            return Ok(regionsDTO); 
        }


        //get region by id
        [HttpGet]
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
        public async Task< IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            if (ModelState.IsValid)
            {
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDTO);

                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel); // Check if the region already exists by ID

                // mapping domain model to DTO
                var createdRegionDTO = mapper.Map<RegionDTO>(regionDomainModel);

                return CreatedAtAction(nameof(GetByID), new { id = createdRegionDTO.Id }, createdRegionDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        //update region
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            if (ModelState.IsValid)
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
            else
            {
                return BadRequest(ModelState);
            }
        }


        //Delete Region
        [HttpDelete]
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