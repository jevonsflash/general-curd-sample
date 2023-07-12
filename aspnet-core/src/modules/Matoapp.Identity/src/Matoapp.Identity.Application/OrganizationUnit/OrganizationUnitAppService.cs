using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Share.ServiceBase;
using Matoapp.Identity.Identity;
using Matoapp.Identity.OrganizationUnit.Dto;
using Matoapp.Identity.OrganizationUnits;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Users;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;


namespace Matoapp.Identity.OrganizationUnit;

public class OrganizationUnitAppService : IdentityAppService, IOrganizationUnitAppService
{
    private readonly ICurrentUser currentUser;
    private readonly IOrganizationUnitRepository organizationUnitRepository;
    private readonly IdentityUserManager identityUserManager;
    private readonly OrganizationUnitsManager organizationUnitsManager;
    private readonly OrganizationUnitManager organizationUnitManager;

    // https://github.com/abpframework/abp/discussions/7502
    private readonly IQueryableIdentityUserRepository queryableIdentityUserRepository;



    public OrganizationUnitAppService(
        ICurrentUser currentUser,
        IOrganizationUnitRepository organizationUnitRepository,
        IdentityUserManager identityUserManager,
        OrganizationUnitsManager organizationUnitsManager,
        OrganizationUnitManager organizationUnitManager,
        IQueryableIdentityUserRepository queryableIdentityUserRepository
        )
    {
        this.currentUser = currentUser;
        this.organizationUnitRepository = organizationUnitRepository;
        this.identityUserManager = identityUserManager;
        this.organizationUnitsManager = organizationUnitsManager;
        this.organizationUnitManager = organizationUnitManager;
        this.queryableIdentityUserRepository = queryableIdentityUserRepository;
    }

    public async Task<OrganizationUnitDto> GetAsync(Guid id)
    {
        var organizationUnit = await organizationUnitRepository.GetAsync(id);
        return ObjectMapper.Map<Volo.Abp.Identity.OrganizationUnit, OrganizationUnitDto>(organizationUnit);
    }

    public async Task<OrganizationUnitDto> CreateAsync(CreateOrganizationUnitInput input)
    {
        input.DisplayName = input.DisplayName.Replace(" ", "");
        var tenantId = currentUser.TenantId;
        var organizationUnit = new Volo.Abp.Identity.OrganizationUnit(GuidGenerator.Create(), input.DisplayName, input.ParentId, tenantId);

        await organizationUnitManager.CreateAsync(organizationUnit);
        await CurrentUnitOfWork.SaveChangesAsync();

        return ObjectMapper.Map<Volo.Abp.Identity.OrganizationUnit, OrganizationUnitDto>(organizationUnit);
    }

    public async Task<OrganizationUnitDto> UpdateAsync(UpdateOrganizationUnitInput input)
    {

        input.DisplayName = input.DisplayName.Replace(" ", "");
        var organizationUnit = await organizationUnitRepository.GetAsync(input.Id);
        organizationUnit.DisplayName = input.DisplayName;

        await organizationUnitManager.UpdateAsync(organizationUnit);

        return ObjectMapper.Map<Volo.Abp.Identity.OrganizationUnit, OrganizationUnitDto>(organizationUnit);
    }




    public virtual async Task DeleteAsync(Guid id)
    {
        var isUsed = await organizationUnitsManager.IsOrganizationUsed(id);
        if (isUsed)
        {
            throw new AbpException($"已经有用户分配于该组织");

        }
        await organizationUnitManager.DeleteAsync(id);
    }


    public async Task MoveOrganizationUnitAsync(MoveOrganizationUnitInput input)
    {
        await organizationUnitManager.MoveAsync(input.Id, input.NewParentId);

    }

    public async Task RemoveUserFromOrganizationUnitAsync(UserToOrganizationUnitInput input)
    {
        await identityUserManager.RemoveFromOrganizationUnitAsync(input.UserId, input.OrganizationUnitId);
    }




    public virtual async Task<bool> IsInOrganizationUnitAsync(UserToOrganizationUnitInput input)
    {
        return await identityUserManager.IsInOrganizationUnitAsync(input.UserId, input.OrganizationUnitId);

    }



    public virtual async Task AddToOrganizationUnitAsync(UserToOrganizationUnitInput input)
    {
        await identityUserManager.AddToOrganizationUnitAsync(input.UserId, input.OrganizationUnitId);

    }


    private async Task<List<OrganizationUnitDto>> _GetOrganizationUnitsAsync(Guid id)
    {
        var currentUser = await identityUserManager.GetByIdAsync(id);
        var organizationUnits = await identityUserManager.GetOrganizationUnitsAsync(currentUser, false);
        var result = ObjectMapper.Map<List<Volo.Abp.Identity.OrganizationUnit>, List<OrganizationUnitDto>>(organizationUnits);
        return result;
    }

    public virtual async Task<List<OrganizationUnitDto>> GetOrganizationUnitsAsync(Guid id)
    {
        return await _GetOrganizationUnitsAsync(id);
    }

    public async Task<List<OrganizationUnitDto>> GetCurrentOrganizationUnitsAsync()
    {
        return await _GetOrganizationUnitsAsync(currentUser.GetId());
    }

    public async Task<OrganizationUnitDto> GetRootOrganizationUnitAsync(Guid id)
    {
        var item = await organizationUnitRepository.FindAsync(id);
        var currentItem = ObjectMapper.Map<Volo.Abp.Identity.OrganizationUnit, OrganizationUnitDto>(item);
        currentItem.Children = await GetOrganizationUnitChildren(item.Id);
        return currentItem;
    }

    public async Task<List<OrganizationUnitDto>> GetRootOrganizationUnitsAsync(IEnumerable<Guid> ids)
    {
        var list = ids == null || ids.Count() == 0 ?
            await organizationUnitRepository.GetListAsync()
            : await organizationUnitRepository.GetListAsync(ids);
        var result = new List<OrganizationUnitDto>();
        foreach (var item in list)
        {
            var currentItem = ObjectMapper.Map<Volo.Abp.Identity.OrganizationUnit, OrganizationUnitDto>(item);
            currentItem.Children = await GetOrganizationUnitChildren(item.Id);
            result.Add(currentItem);
        }
        return result;
    }

    public async Task<OrganizationUnitDto> GetRootOrganizationUnitByDisplayNameAsync(GetRootOrganizationUnitByDisplayName input)
    {
        var item = await organizationUnitRepository.GetAsync(input.DisplayName);
        var currentItem = ObjectMapper.Map<Volo.Abp.Identity.OrganizationUnit, OrganizationUnitDto>(item);
        currentItem.Children = await GetOrganizationUnitChildren(item.Id);
        return currentItem;
    }

    public async Task<List<OrganizationUnitDto>> GetRootOrganizationUnitsByParentAsync(GetRootOrganizationUnitsByParentInput input)
    {
        var parentId = await GetParentId(input);
        var list = await organizationUnitRepository.GetChildrenAsync(parentId);
        var result = new List<OrganizationUnitDto>();
        foreach (var item in list)
        {
            var currentItem = ObjectMapper.Map<Volo.Abp.Identity.OrganizationUnit, OrganizationUnitDto>(item);

            currentItem.Children = await GetOrganizationUnitChildren(item.Id);
            result.Add(currentItem);
        }
        return result;
    }


    public async Task<List<IdentityUserDto>> GetOrganizationUnitUsersAsync(GetOrganizationUnitUsersInput input)
    {
        var query = await queryableIdentityUserRepository.GetOrganizationUnitUsersAsync(input.Id, input.Keyword, input.Type);

        var totalCount = query.Count();
        var users = query
            .OrderBy(u => u.Name)
            .ThenBy(u => u.Surname)
            .ToList();

        var result = ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(users);
        return result;
    }


    public async Task<PagedResultDto<IdentityUserDto>> GetOrganizationUnitUsersByPageAsync(GetOrganizationUnitUsersInput input)
    {
        var query = await queryableIdentityUserRepository.GetOrganizationUnitUsersAsync(input.Id, input.Keyword, input.Type);


        var totalCount = query.Count();
        var users = query
            .OrderBy(u => u.Name)
            .ThenBy(u => u.Surname)
            .PageBy(input)
            .ToList();

        var result = ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(users);

        return new PagedResultDto<IdentityUserDto>(
            totalCount, result
           );
    }


    public async Task<List<IdentityUserDto>> GetUsersWithoutOrganizationAsync(GetUserWithoutOrganizationInput input)
    {


        var query = await queryableIdentityUserRepository.GetUsersWithoutOrganizationAsync(input.Keyword, input.Type);


        var userCount = query.Count();
        var users = query
            .OrderBy(u => u.Name)
            .ThenBy(u => u.Surname)
            .ToList();
        var result = ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(users);
        return result;
    }


    public async Task<PagedResultDto<IdentityUserDto>> GetUsersWithoutOrganizationByPageAsync(GetUserWithoutOrganizationInput input)
    {
        var query = await queryableIdentityUserRepository.GetUsersWithoutOrganizationAsync(input.Keyword, input.Type);
        var userCount = query.Count();
        var users = query
            .OrderBy(u => u.Name)
            .ThenBy(u => u.Surname)
            .PageBy(input)
            .ToList();
        var result = ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(users);
        return new PagedResultDto<IdentityUserDto>(
            userCount, result
            );
    }


    private Expression<Func<IdentityUser, bool>> FilterByKeyword(string keyword)
    {
        return user => (user.Surname + user.Name).Contains(keyword);
    }

    private async Task<List<OrganizationUnitDto>> GetOrganizationUnitChildren(Guid parentId)
    {
        var list = await organizationUnitRepository.GetChildrenAsync(parentId);

        var result = new List<OrganizationUnitDto>();
        foreach (var item in list)
        {
            var currentItem = ObjectMapper.Map<Volo.Abp.Identity.OrganizationUnit, OrganizationUnitDto>(item);

            currentItem.Children = await GetOrganizationUnitChildren(item.Id);
            result.Add(currentItem);
        }
        return result;
    }

    private async Task<Guid?> GetParentId(GetRootOrganizationUnitsByParentInput input)
    {
        Guid? parentId;
        if (!input.ParentId.HasValue && !string.IsNullOrEmpty(input.ParentName))
        {
            var parentOrganization = await organizationUnitRepository.GetAsync(input.ParentName);
            parentId = parentOrganization?.Id;
        }
        else
        {
            parentId = input.ParentId;
        }

        return parentId;
    }




}
