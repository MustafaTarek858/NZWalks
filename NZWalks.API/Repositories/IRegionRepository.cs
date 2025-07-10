using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using System.Threading.Tasks;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
         Task<List<Region>> GetAllAsync();

        Task<Region?> GetByIdAsync( Guid id);

        Task<Region> CreateAsync( Region regionDomainmodel);
            
        Task <Region?> DeleteAsync(Guid id);

        Task<Region?> UpdateAsync(Guid id,Region regionDomainModel);
    }
}
