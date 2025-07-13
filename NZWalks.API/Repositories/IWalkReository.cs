using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkReository
    {
       Task<Walk> CreateAsync(Walk walk);

       Task<Walk?> GetByIdAsync(Guid id);

       Task<List<Walk>> GetAllAsync();
    }
}
