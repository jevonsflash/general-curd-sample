using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Matoapp.Identity.Identity
{
    public interface IQueryableIdentityUserRepository : IIdentityUserRepository
    {
        Task<IQueryable<OrganizationUnit>> GetOrganizationUnitsQueryableAsync(Guid id, bool includeDetails = false);
        Task<IQueryable<IdentityUser>> GetOrganizationUnitUsersAsync(
         Guid id, string keyword, string[] type,
         bool includeDetails = false);
        Task<IQueryable<IdentityUser>> GetUsersWithoutOrganizationAsync(string keyword, string[] type);
    }
}
