using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAll()
        {
            // Gettting all the data from the domain model
            var regions = dbcontext.Regions.ToList(); // Fetches all regions from the database

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
        public IActionResult GetByID([FromRoute]Guid id)
        {
            //var region = dbcontext.Regions.Find(id);

            //another way by using LinQ
            var region = dbcontext.Regions.FirstOrDefault(x => x.Id == id);

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
        public IActionResult Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            // map or convert DTO to domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl=addRegionRequestDTO.RegionImageUrl

            };

            //Use domain model to create region
            dbcontext.Regions.Add(regionDomainModel);
            dbcontext.SaveChanges();

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
        public IActionResult Update([FromRoute] Guid id , [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            var regionDomainModel = dbcontext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            // Map or convert DTO to domain model
            regionDomainModel.Name = updateRegionRequestDTO.Name;
            regionDomainModel.Code = updateRegionRequestDTO.Code;
            regionDomainModel.RegionImageUrl = updateRegionRequestDTO.RegionImageUrl;

            dbcontext.SaveChanges();

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

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {

            var regionDomainModel = dbcontext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            dbcontext.Regions.Remove(regionDomainModel);
            dbcontext.SaveChanges();

            // Return a 200 OK response indicating successful deletion
            return Ok();
        }
    }
}