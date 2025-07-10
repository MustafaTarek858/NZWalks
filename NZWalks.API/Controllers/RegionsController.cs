using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;

        public RegionsController( IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository; // Assigning the provided repository to the class field
        }


        //get all regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Gettting all the data from the domain model
            var regions = await regionRepository.GetAllAsync(); // Fetches all regions from the database

            //adding all the data to the DTO
            var regionsDTO = new List<RegionDTO>();
            foreach (var region in regions) 
            {
                regionsDTO.Add
                (
                    new RegionDTO()
                    {
                        Id = region.Id,
                        Name = region.Name,
                        Code = region.Code,
                        RegionImageUrl = region.RegionImageUrl
                    }
                );
            }


            return Ok(regionsDTO); // Returns a 200 OK response with the list of regions
        }


        //get region by id
        [HttpGet]
        [Route("{id:guid}")]
        public async Task <IActionResult> GetByID([FromRoute]Guid id)
        {
            //var region = dbcontext.Regions.Find(id);

            //another way by using LinQ
            var region = await regionRepository.GetByIdAsync(id); // Fetches the region with the specified ID or null if not found  

            if (region == null)
            {
                return NotFound();
            }

            var regionsDTO = new RegionDTO
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl

            };

           
            return Ok(regionsDTO);
        }


        // create region
        [HttpPost]
        public async Task< IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            // map or convert DTO to domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl=addRegionRequestDTO.RegionImageUrl

            };

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel); // Check if the region already exists by ID

            // Return the created region as DTO
            var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetByID), new { id = regionDTO.Id }, regionDTO);
        }


        //update region
        [HttpPut]
        [Route("{id:guid}")]
        public async Task < IActionResult> Update([FromRoute] Guid id , [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            //maping domain models
            var regionDomainModel = new Region
            {
                Id = id, // Set the ID of the region to be updated
                Name = updateRegionRequestDTO.Name,
                Code = updateRegionRequestDTO.Code,
                RegionImageUrl = updateRegionRequestDTO.RegionImageUrl
            };

            // Update the region in the database
            var updatedRegion = await regionRepository.UpdateAsync(id,regionDomainModel); // Updates the region in the database

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //maping DTOs
            var updatedRegionDTO = new RegionDTO
            {
                Id = updatedRegion.Id,
                Name = updatedRegion.Name,
                Code = updatedRegion.Code,
                RegionImageUrl = updatedRegion.RegionImageUrl
            };



            return Ok(updatedRegionDTO);
        }


        //Delete Region
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task < IActionResult > Delete([FromRoute] Guid id)
        {

            var deletedRegion = await regionRepository.DeleteAsync(id); // Deletes the region with the specified ID
            if (deletedRegion == null)
            {
                return NotFound(); // If the region does not exist, return a 404 Not Found response
            }
            //maping DTOs
            var deletedRegionDTO = new RegionDTO
            {
                Id = deletedRegion.Id,
                Name = deletedRegion.Name,
                Code = deletedRegion.Code,
                RegionImageUrl = deletedRegion.RegionImageUrl
            };

            // Return a 200 OK response indicating successful deletion
            return Ok(deletedRegionDTO);
        }
    }
}