using Microsoft.AspNetCore.Mvc;
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

        public async  Task<List<Walk>> GetAllAsync
            (
            [FromQuery] string? filterOn = null, 
            [FromQuery] string? filterQuery = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool isAscending = true 
            )
        {
            var walks = DbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false )
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name): walks.OrderByDescending(x => x.Name);
                }else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            return await walks.ToListAsync();
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


            existingWalk.Name = walk.Name;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.Description = walk.Description;
            existingWalk.DifficultyId = walk.DifficultyId;


            await DbContext.SaveChangesAsync();
            return existingWalk;

        }

        async public Task<Walk?> DeleteAsync(Guid id)
        {
            var walkDomain = await DbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDomain == null)
            {
                return null;
            }
            DbContext.Walks.Remove(walkDomain);
            await DbContext.SaveChangesAsync();
            return walkDomain; // Return the deleted walk
        }
    }
}
