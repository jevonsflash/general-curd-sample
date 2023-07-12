using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Share.Web.Models;
using Matoapp.Identity.OrganizationUnit.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace Matoapp.Identity.OrganizationUnit
{
    [Area(IdentityRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Route("api/identity/organizationUnit")]
    public class OrganizationUnitController : IdentityController, IOrganizationUnitAppService
    {
        private readonly IOrganizationUnitAppService _organizationUnitAppService;

        public OrganizationUnitController(IOrganizationUnitAppService organizationUnitAppService)
        {
            _organizationUnitAppService = organizationUnitAppService;
        }

        [HttpPost]
        [Route("AddToOrganizationUnit")]
        
        public async Task AddToOrganizationUnitAsync(UserToOrganizationUnitInput input)
        {
            await _organizationUnitAppService.AddToOrganizationUnitAsync(input);
        }

        [HttpPost]
        [Route("Create")]
        
        public async Task<OrganizationUnitDto> CreateAsync(CreateOrganizationUnitInput input)
        {
            return await _organizationUnitAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("Delete")]
        
        public async Task DeleteAsync(Guid id)
        {
            await _organizationUnitAppService.DeleteAsync(id);
        }


        [HttpGet]
        [Route("Get")]
        
        public async Task<OrganizationUnitDto> GetAsync(Guid id)
        {
            return await _organizationUnitAppService.GetAsync(id);

        }

        [HttpGet]
        [Route("GetCurrentOrganizationUnits")]

        
        public async Task<List<OrganizationUnitDto>> GetCurrentOrganizationUnitsAsync()
        {
            return await _organizationUnitAppService.GetCurrentOrganizationUnitsAsync();
        }


        [HttpGet]
        [Route("GetOrganizationUnitUsers")]
        
        public async Task<List<IdentityUserDto>> GetOrganizationUnitUsersAsync(GetOrganizationUnitUsersInput input)
        {
            return await _organizationUnitAppService.GetOrganizationUnitUsersAsync(input);
        }

        [HttpGet]
        [Route("GetOrganizationUnitUsersByPage")]
        
        public async Task<PagedResultDto<IdentityUserDto>> GetOrganizationUnitUsersByPageAsync(GetOrganizationUnitUsersInput input)
        {
            return await _organizationUnitAppService.GetOrganizationUnitUsersByPageAsync(input);
        }

        [HttpGet]
        [Route("GetRootOrganizationUnit")]
        
        public async Task<OrganizationUnitDto> GetRootOrganizationUnitAsync(Guid id)
        {
            return await _organizationUnitAppService.GetRootOrganizationUnitAsync(id);
        }

        [HttpGet]
        [Route("GetRootOrganizationUnits")]
        
        public async Task<List<OrganizationUnitDto>> GetRootOrganizationUnitsAsync(IEnumerable<Guid> ids)
        {
            return await _organizationUnitAppService.GetRootOrganizationUnitsAsync(ids);
        }

        [HttpGet]
        [Route("GetRootOrganizationUnitByDisplayName")]
        
        public async Task<OrganizationUnitDto> GetRootOrganizationUnitByDisplayNameAsync(GetRootOrganizationUnitByDisplayName input)
        {
            return await _organizationUnitAppService.GetRootOrganizationUnitByDisplayNameAsync(input);
        }

        [HttpGet]
        [Route("GetRootOrganizationUnitsByParent")]
        
        public async Task<List<OrganizationUnitDto>> GetRootOrganizationUnitsByParentAsync(GetRootOrganizationUnitsByParentInput input)
        {
            return await _organizationUnitAppService.GetRootOrganizationUnitsByParentAsync(input);
        }

        [HttpGet]
        [Route("GetUsersWithoutOrganization")]
        
        public async Task<List<IdentityUserDto>> GetUsersWithoutOrganizationAsync(GetUserWithoutOrganizationInput input)
        {
            return await _organizationUnitAppService.GetUsersWithoutOrganizationAsync(input);
        }

        [HttpGet]
        [Route("GetUsersWithoutOrganizationByPage")]
        
        public async Task<PagedResultDto<IdentityUserDto>> GetUsersWithoutOrganizationByPageAsync(GetUserWithoutOrganizationInput input)
        {
            return await _organizationUnitAppService.GetUsersWithoutOrganizationByPageAsync(input);
        }

        [HttpGet]
        [Route("IsInOrganizationUnit")]
        
        public async Task<bool> IsInOrganizationUnitAsync(UserToOrganizationUnitInput input)
        {
            return await _organizationUnitAppService.IsInOrganizationUnitAsync(input);
        }

        [HttpPost]
        [Route("MoveOrganizationUnit")]
        
        public async Task MoveOrganizationUnitAsync(MoveOrganizationUnitInput input)
        {
            await _organizationUnitAppService.MoveOrganizationUnitAsync(input);
        }

        [HttpPost]
        [Route("RemoveUserFromOrganizationUnit")]
        
        public async Task RemoveUserFromOrganizationUnitAsync(UserToOrganizationUnitInput input)
        {
            await _organizationUnitAppService.RemoveUserFromOrganizationUnitAsync(input);
        }

        [HttpPut]
        [Route("Update")]
        
        public async Task<OrganizationUnitDto> UpdateAsync(UpdateOrganizationUnitInput input)
        {
            return await _organizationUnitAppService.UpdateAsync(input);
        }

    }
}