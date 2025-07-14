using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkReository walkRepo;

        public WalksController(IMapper mapper, IWalkReository walkRepo)
        {
            this.mapper = mapper;
            this.walkRepo = walkRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walkDomainModels = await walkRepo.GetAllAsync();
            var walkDTOs = mapper.Map<List<walkDTO>>(walkDomainModels);
            return Ok(walkDTOs);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task< IActionResult> GetById([FromRoute]Guid id)
        {
            var walkDomainModel = await walkRepo.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            var WalkDTO = mapper.Map<walkDTO>(walkDomainModel);
            return Ok(WalkDTO);
        }

        [HttpPost]
        public async Task< IActionResult> Create([FromBody] AddWalkRequestDTO WalkDto )
        {
            //mapping DomainModel 
            var walkDomainModel = mapper.Map<Walk>(WalkDto);
            var addedWalkDOmainModel = await walkRepo.CreateAsync(walkDomainModel);
            var walkDTO =  mapper.Map<walkDTO>(addedWalkDOmainModel);

            return CreatedAtAction(nameof(GetById), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromBody] UpdateRegionRequestDTO updateWalkDTO , [FromRoute] Guid id)
        {
            var wlakDomainModel = mapper.Map<Walk>(updateWalkDTO);
            var updatedWalkDomainModel = await  walkRepo.UpdateAsync(id, wlakDomainModel);
            if (updatedWalkDomainModel == null)
            {
                return NotFound();
            }
            var updatedWalkDTO =  mapper.Map<UpdateWalkRequestDTO>(updatedWalkDomainModel);

            return Ok(updatedWalkDTO);
        }
    }
}
