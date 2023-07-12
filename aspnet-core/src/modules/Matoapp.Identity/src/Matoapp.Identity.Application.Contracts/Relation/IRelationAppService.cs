using Matoapp.Identity.Relation.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Matoapp.Identity.Relation
{
    public interface IRelationAppService : IApplicationService
    {
        Task ClearAllRelatedFromUsersAsync(GetRelatedUsersInput input);
        Task ClearAllRelatedToUsersAsync(GetRelatedUsersInput input);
        Task<RelationDto> CreateAsync(ModifyRelationInput input);
        Task DeleteAsync(EntityDto<long> input);
        Task DeleteByUserIdAsync(ModifyRelationInput input);
        Task<List<IdentityUserDto>> GetRelatedFromUsersAsync(GetRelatedUsersInput input);
        Task<List<IdentityUserDto>> GetRelatedToUsersAsync(GetRelatedUsersInput input);

        Task<List<Guid>> GetRelatedToUserIdsAsync(GetRelatedUsersInput input);

        Task<List<Guid>> GetRelatedFromUserIdsAsync(GetRelatedUsersInput input);

    }
}