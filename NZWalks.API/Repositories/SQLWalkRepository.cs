using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkReository
    {
        private readonly NZWalksDbContext DbContext;
        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        async public Task<List<Walk>> GetAllAsync()
        {
            return await DbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        async public Task<Walk?> GetByIdAsync(Guid id)
        {
            return await DbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        async public Task<Walk> CreateAsync(Walk walk)
        {
            await DbContext.Walks.AddAsync(walk);
            await DbContext.SaveChangesAsync();
            return walk;
        }

        async public Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await GetByIdAsync(id);
            if (existingWalk == null)
            {
                return null; // Walk not found
            }
            // Update the properties of the existing walk


            existingWalk.Id = walk.Id;
            existingWalk.Name = walk.Name;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.Description = walk.Description;
            existingWalk.Difficulty = walk.Difficulty;
            existingWalk.Region = walk.Region;


            await DbContext.SaveChangesAsync();
            return existingWalk;

        }
    }
}
