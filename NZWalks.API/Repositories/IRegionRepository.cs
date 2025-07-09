using Microsoft.Identity.Client;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
         Task<List<Region>> GetAllAsync();
    }
}
