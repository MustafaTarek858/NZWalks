using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

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
    }
}
