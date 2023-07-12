using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matoapp.Identity.Relation.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Matoapp.Identity.Relation;

public class RelationAppService : IdentityAppService, IRelationAppService
{

    private readonly IRepository<Relation> Repository;
    private readonly RelationManager _relationManager;

    public RelationAppService(
        IRepository<Relation> _repository,
        RelationManager relationManager
        )
    {
        Repository = _repository;
        _relationManager = relationManager;
    }


    public async Task<List<IdentityUserDto>> GetRelatedToUsersAsync(GetRelatedUsersInput input)
    {
        var items = await _relationManager.GetRelatedToUsersWidthDetailAsync(input.UserId, input.Type);
        var result = ObjectMapper.Map<List<Relation>, List<RelationDto>>(items).Select(c => c.RelatedUser).ToList();
        return result;
    }

    public async Task<List<IdentityUserDto>> GetRelatedFromUsersAsync(GetRelatedUsersInput input)
    {
        var items = await _relationManager.GetRelatedFromUsersWidthDetailAsync(input.UserId, input.Type);
        var result = ObjectMapper.Map<List<Relation>, List<RelationDto>>(items).Select(c => c.User).ToList();
        return result;
    }

    public async Task<List<Guid>> GetRelatedToUserIdsAsync(GetRelatedUsersInput input)
    {
        var items = await _relationManager.GetRelatedToUsersAsync(input.UserId, input.Type);
        var result = items.Select(c => c.RelatedUserId).ToList();
        return result;
    }

    public async Task<List<Guid>> GetRelatedFromUserIdsAsync(GetRelatedUsersInput input)
    {
        var items = await _relationManager.GetRelatedFromUsersAsync(input.UserId, input.Type);
        var result = items.Select(c => c.UserId).ToList();
        return result;
    }


    public virtual async Task<RelationDto> CreateAsync(ModifyRelationInput input)
    {
        var entity = ObjectMapper.Map<ModifyRelationInput, Relation>(input);

        await Repository.InsertAsync(entity);
        await CurrentUnitOfWork.SaveChangesAsync();

        return ObjectMapper.Map<Relation, RelationDto>(entity);
    }


    public virtual async Task DeleteAsync(EntityDto<long> input)
    {
        await Repository.DeleteAsync(c => c.Id == input.Id);
    }

    public async Task ClearAllRelatedToUsersAsync(GetRelatedUsersInput input)
    {
        await _relationManager.ClearAllRelatedToUsersAsync(input.UserId, input.Type);
    }


    public async Task ClearAllRelatedFromUsersAsync(GetRelatedUsersInput input)
    {
        await _relationManager.ClearAllRelatedFromUsersAsync(input.UserId, input.Type);
    }

    public async Task DeleteByUserIdAsync(ModifyRelationInput input)
    {
        await _relationManager.DeleteByUserId(input.UserId, input.RelatedUserId, input.Type);
    }

}

