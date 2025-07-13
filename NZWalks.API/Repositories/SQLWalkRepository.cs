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
            return await DbContext.Walks.ToListAsync(); 
        }

        async public Task<Walk?> GetByIdAsync(Guid id) 
        {
            return await DbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        }

        async public Task<Walk> CreateAsync(Walk walk)
        {
            await DbContext.Walks.AddAsync(walk);
            await DbContext.SaveChangesAsync();
            return walk;
        }


    }
}
