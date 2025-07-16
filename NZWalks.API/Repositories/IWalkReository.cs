using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkReository
    {
       Task<Walk> CreateAsync(Walk walk);

       Task<Walk?> GetByIdAsync(Guid id);

       Task<List<Walk>> GetAllAsync([FromQuery] string? filterOn = null, [FromQuery] string? filterQuery = null);

        Task<Walk?> UpdateAsync(Guid id, Walk walk);

        Task<Walk?> DeleteAsync(Guid id);
    }
}
