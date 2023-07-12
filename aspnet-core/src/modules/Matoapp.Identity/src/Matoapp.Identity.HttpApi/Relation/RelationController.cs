using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Share.Web.Models;
using Matoapp.Identity.Relation.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace Matoapp.Identity.Relation
{
    [Area(IdentityRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Route("api/identity/relation")]
    public class RelationController : IdentityController, IRelationAppService
    {
        private readonly IRelationAppService _relationAppService;

        public RelationController(IRelationAppService relationAppService)
        {
            _relationAppService = relationAppService;
        }

        [HttpDelete]
        [Route("ClearAllRelatedFromUsers")]

        public async Task ClearAllRelatedFromUsersAsync(GetRelatedUsersInput input)
        {
            await _relationAppService.ClearAllRelatedFromUsersAsync(input);
        }

        [HttpDelete]
        [Route("ClearAllRelatedToUsers")]

        public async Task ClearAllRelatedToUsersAsync(GetRelatedUsersInput input)
        {
            await _relationAppService.ClearAllRelatedToUsersAsync(input);
        }

        [HttpPost]
        [Route("Create")]

        public async Task<RelationDto> CreateAsync(ModifyRelationInput input)
        {
            return await _relationAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("Delete")]

        public async Task DeleteAsync(EntityDto<long> input)
        {
            await _relationAppService.DeleteAsync(input);
        }

        [HttpDelete]
        [Route("DeleteByUserId")]

        public async Task DeleteByUserIdAsync(ModifyRelationInput input)
        {
            await _relationAppService.DeleteByUserIdAsync(input);
        }

        [HttpGet]
        [Route("GetRelatedFromUsers")]

        public async Task<List<IdentityUserDto>> GetRelatedFromUsersAsync(GetRelatedUsersInput input)
        {
            return await _relationAppService.GetRelatedFromUsersAsync(input);
        }

        [HttpGet]
        [Route("GetRelatedToUsers")]

        public async Task<List<IdentityUserDto>> GetRelatedToUsersAsync(GetRelatedUsersInput input)
        {
            return await _relationAppService.GetRelatedToUsersAsync(input);
        }

        [HttpGet]
        [Route("GetRelatedToUserIds")]
        public async Task<List<Guid>> GetRelatedToUserIdsAsync(GetRelatedUsersInput input)
        {
            return await _relationAppService.GetRelatedToUserIdsAsync(input);
        }


        [HttpGet]
        [Route("GetRelatedFromUserIds")]
        public async Task<List<Guid>> GetRelatedFromUserIdsAsync(GetRelatedUsersInput input)
        {
            return await _relationAppService.GetRelatedFromUserIdsAsync(input);
        }


    }
}