//using System.Data.Entity;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matoapp.Identity.OrganizationUnit.Dto
{
    public interface IRootOrganizationQueryService<TEntityDto>
    {
        Task<List<TEntityDto>> GetRootOrganizationUnit(GetRootOrganizationUnitsByParentInput input);
    }
}