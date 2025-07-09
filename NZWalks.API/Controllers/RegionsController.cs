using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbcontext; // Database context for accessing the database
        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbcontext = dbContext;
        }


        //get all regions
        [HttpGet]
        public async Task< IActionResult> GetAll()
        {
            // Gettting all the data from the domain model
            var regions = await dbcontext.Regions.ToListAsync(); // Fetches all regions from the database

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
        public async Task < IActionResult > GetByID([FromRoute]Guid id)
        {
            //var region = dbcontext.Regions.Find(id);

            //another way by using LinQ
            var region = await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);

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

            //Use domain model to create region
           await dbcontext.Regions.AddAsync(regionDomainModel);
           await dbcontext.SaveChangesAsync();

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
            var regionDomainModel = await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            // Map or convert DTO to domain model
            regionDomainModel.Name = updateRegionRequestDTO.Name;
            regionDomainModel.Code = updateRegionRequestDTO.Code;
            regionDomainModel.RegionImageUrl = updateRegionRequestDTO.RegionImageUrl;

            await dbcontext.SaveChangesAsync();

            // Return the updated region as DTO
            var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDTO);
        }


        //Delete Region
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task < IActionResult > Delete([FromRoute] Guid id)
        {

            var regionDomainModel = await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            dbcontext.Regions.Remove(regionDomainModel);
           await dbcontext.SaveChangesAsync();

            // Return a 200 OK response indicating successful deletion
            return Ok();
        }
    }
}