
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;


namespace Matoapp.Identity.OrganizationUnits
{
    public class OrganizationUnitsManager : DomainService
    {
        private readonly IRepository<IdentityUserOrganizationUnit> _userOrganizationUnitRepository;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;


        public OrganizationUnitsManager(

            IRepository<IdentityUserOrganizationUnit> userOrganizationUnitRepository,
                   IOrganizationUnitRepository organizationUnitRepository

            )
        {
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _organizationUnitRepository = organizationUnitRepository;
        }

        public async Task<OrganizationUnit> GetUserOrganizationUnitAsync(Guid userId)
        {
            var userOrganizationUnit = await _userOrganizationUnitRepository
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (userOrganizationUnit == null)
            {
                throw new UserFriendlyException("找不到用户单位");
            }

            var organizationUnit = await _organizationUnitRepository.GetAsync(userOrganizationUnit.OrganizationUnitId);

            if (organizationUnit == null)
            {
                throw new UserFriendlyException("找不到用户单位");
            }

            return organizationUnit;
        }

        public virtual async Task<bool> IsOrganizationUsed(Guid organizationUnitId)
        {
            var userlistQueryable = await _userOrganizationUnitRepository.GetQueryableAsync();
            var userlist = userlistQueryable.Where(u => u.OrganizationUnitId == organizationUnitId);
            return userlist.Any();
        }

    }
}
