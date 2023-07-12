using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Application.Share.ServiceBase;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;
using Volo.Abp.Identity;
using Matoapp.Identity.OrganizationUnit;
using Matoapp.Identity.OrganizationUnit.Dto;
using Volo.Abp;
using Matoapp.Health.Client.Dto;
using Matoapp.Identity.Relation;
using Matoapp.Identity.Relation.Dto;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Security.Claims;

namespace Matoapp.Health.Client

{
    //TEntity, TGetOutputDto, TGetListOutputDto, TGetListBriefOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput
    public class ClientAppService : CurdAppServiceBase<Client, ClientDto, Guid, GetAllClientInput, CreateClientInput>, IClientAppService
    {
        private readonly IOrganizationUnitAppService organizationUnitAppService;
        private readonly ICurrentUser currentUser;
        private readonly IRelationAppService relationAppService;
        private readonly IClientLookupService userLookupService;
        private readonly IIdentityUserAppService identityUserAppService;

        public ClientAppService(
            IOrganizationUnitAppService organizationUnitAppService,
            ICurrentUser currentUser,
            IRelationAppService relationAppService,
            IClientLookupService userLookupService,
            IIdentityUserAppService identityUserAppService,
            IRepository<Client, Guid> basicInventoryRepository) : base(basicInventoryRepository)
        {
            this.organizationUnitAppService = organizationUnitAppService;
            this.currentUser = currentUser;
            this.relationAppService = relationAppService;
            this.userLookupService = userLookupService;
            this.identityUserAppService = identityUserAppService;
        }

        protected override async Task<IQueryable<Client>> DefaultConvention(GetAllClientInput input, IQueryable<Client> query)
        {

            if (input.OrganizationUnitId.HasValue && !input.IsWithoutOrganization)
            {
                var organizationUnitUsers = await organizationUnitAppService.GetOrganizationUnitUsersAsync(new GetOrganizationUnitUsersInput()
                {
                    Id = input.OrganizationUnitId.Value
                });
                if (organizationUnitUsers.Count() > 0)
                {
                    var ids = organizationUnitUsers.Select(c => c.Id);
                    query = query.Where(t => ids.Contains(t.Id));
                }
                else
                {
                    query = query.Where(c => false);
                }
            }
            else if (input.IsWithoutOrganization)
            {
                var organizationUnitUsers = await organizationUnitAppService.GetUsersWithoutOrganizationAsync(new GetUserWithoutOrganizationInput());
                if (organizationUnitUsers.Count() > 0)
                {
                    var ids = organizationUnitUsers.Select(c => c.Id);
                    query = query.Where(t => ids.Contains(t.Id));
                }
                else
                {
                    query = query.Where(c => false);
                }
            }

            if (input.RelationToUserId.HasValue && !string.IsNullOrEmpty(input.RelationType))
            {
                Guid userId = default;
                if (input.RelationToUserId.Value == Guid.Empty)
                {
                    userId = currentUser.GetId();
                }
                else
                {
                    userId = input.RelationToUserId.Value;
                }

                var relatedToUserIds = await relationAppService.GetRelatedToUserIdsAsync(new GetRelatedUsersInput()
                {
                    UserId = userId,
                    Type = input.RelationType
                });
                if (relatedToUserIds.Count() > 0)
                {
                    query = query.Where(t => relatedToUserIds.Contains(t.Id));
                }
                else
                {
                    query = query.Where(c => false);
                }
            }

            query = ApplySearchFiltered(query, input);

            return query;
        }

        public async Task<ClientDto> CreateWithUserAsync(CreateClientWithUserInput input)
        {

            var createdUser = await identityUserAppService.CreateAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync();
            var currentClient = await userLookupService.FindByIdAsync(createdUser.Id);
            ObjectMapper.Map(input, currentClient);
            var updatedClient = await Repository.UpdateAsync(currentClient);
            var result = ObjectMapper.Map<Client, ClientDto>(updatedClient);

            if (input.OrganizationUnitId.HasValue)
            {
                await organizationUnitAppService.AddToOrganizationUnitAsync(
                    new UserToOrganizationUnitInput()
                    { UserId = createdUser.Id, OrganizationUnitId = input.OrganizationUnitId.Value });
            }
            return result;
        }



        public async Task<ClientDto> UpdateWithUserAsync(CreateClientInput input)
        {

            var currentClient = await userLookupService.FindByIdAsync(input.Id);
            if (currentClient == null)
            {
                throw new UserFriendlyException("没有找到对应的用户");
            }
            ObjectMapper.Map(input, currentClient);
            var updatedClient = await Repository.UpdateAsync(currentClient);
            var result = ObjectMapper.Map<Client, ClientDto>(updatedClient);

            return result;
        }
    }

}
