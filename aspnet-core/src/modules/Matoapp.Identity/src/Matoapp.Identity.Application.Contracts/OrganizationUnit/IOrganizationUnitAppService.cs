using Application.Share.Services;
using Matoapp.Identity.OrganizationUnit.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Matoapp.Identity.OrganizationUnit
{
    public interface IOrganizationUnitAppService : IBasicCurdAppService<OrganizationUnitDto, Guid, CreateOrganizationUnitInput, UpdateOrganizationUnitInput>, IApplicationService
    {
        Task AddToOrganizationUnitAsync(UserToOrganizationUnitInput input);
        Task<List<OrganizationUnitDto>> GetCurrentOrganizationUnitsAsync();
        Task<PagedResultDto<IdentityUserDto>> GetOrganizationUnitUsersByPageAsync(GetOrganizationUnitUsersInput input);
        Task<List<IdentityUserDto>> GetOrganizationUnitUsersAsync(GetOrganizationUnitUsersInput input);
        Task<OrganizationUnitDto> GetRootOrganizationUnitAsync(Guid id);
        Task<List<OrganizationUnitDto>> GetRootOrganizationUnitsAsync(IEnumerable<Guid> ids);
        Task<OrganizationUnitDto> GetRootOrganizationUnitByDisplayNameAsync(GetRootOrganizationUnitByDisplayName input);
        Task<List<OrganizationUnitDto>> GetRootOrganizationUnitsByParentAsync(GetRootOrganizationUnitsByParentInput input);
        Task<bool> IsInOrganizationUnitAsync(UserToOrganizationUnitInput input);
        Task MoveOrganizationUnitAsync(MoveOrganizationUnitInput input);
        Task RemoveUserFromOrganizationUnitAsync(UserToOrganizationUnitInput input);
        Task<List<IdentityUserDto>> GetUsersWithoutOrganizationAsync(GetUserWithoutOrganizationInput input);
        Task<PagedResultDto<IdentityUserDto>> GetUsersWithoutOrganizationByPageAsync(GetUserWithoutOrganizationInput input);
    }
}