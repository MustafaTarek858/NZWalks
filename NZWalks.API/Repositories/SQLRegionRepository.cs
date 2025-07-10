using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbcontext; // Database context for accessing the database

        public SQLRegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.dbcontext = nZWalksDbContext; // Assigning the provided database context to the class field
        }


        public async Task<List<Region>> GetAllAsync()
        {
            return  await dbcontext.Regions.ToListAsync(); // Fetches all regions from the database
        }

        public async Task<Region?> GetByIdAsync(Guid id) {
             
            return await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);  // Returns the region with the specified ID or null if not found
        }

        public async Task<Region> CreateAsync(Region regionDomainModel)
        {
            await dbcontext.Regions.AddAsync(regionDomainModel);
            await dbcontext.SaveChangesAsync();
            return regionDomainModel;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null; // If the region does not exist, simply return
            }
            dbcontext.Regions.Remove(existingRegion);
            await dbcontext.SaveChangesAsync(); // Save changes to the database
            return existingRegion;
        }

        public async Task<Region?> UpdateAsync(Guid id,Region regionDomainModel)
        {
            var existingRegion = await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == regionDomainModel.Id);
            if (existingRegion == null)
            {
                return null; // Return null if the region does not exist
            }

            // Update the existing region's properties
            existingRegion.Name = regionDomainModel.Name;
            existingRegion.Code = regionDomainModel.Code;
            existingRegion.RegionImageUrl = regionDomainModel.RegionImageUrl;

            // Save the changes to the database
            await dbcontext.SaveChangesAsync();

            return existingRegion; // Return the updated region
        }

    }
}
