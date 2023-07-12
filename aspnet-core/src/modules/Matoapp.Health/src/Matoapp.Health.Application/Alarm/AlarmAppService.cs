using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Matoapp.Health.Alarm.Dto;
using Application.Share.ServiceBase;
using Volo.Abp.Application.Dtos;
using System;
using Matoapp.Identity.OrganizationUnit;
using Matoapp.Identity.OrganizationUnit.Dto;
using Matoapp.Identity.Relation;
using Matoapp.Identity.Relation.Dto;

namespace Matoapp.Health.Alarm
{
    public class AlarmAppService : ExtendedCurdAppServiceBase<Matoapp.Health.Alarm.Alarm, AlarmDto, AlarmDto, AlarmBriefDto, long, GetAllAlarmInput, GetAllAlarmInput, CreateAlarmInput, UpdateAlarmInput>, IAlarmAppService
    {
        private readonly IOrganizationUnitAppService organizationUnitAppService;
        private readonly IRelationAppService relationAppService;

        public AlarmAppService(
            IOrganizationUnitAppService organizationUnitAppService,
            IRelationAppService relationAppService,
            IRepository<Matoapp.Health.Alarm.Alarm, long> basicInventoryRepository) : base(basicInventoryRepository)
        {
            this.organizationUnitAppService = organizationUnitAppService;
            this.relationAppService = relationAppService;
        }

        protected override async Task<IQueryable<Alarm>> CreateFilteredQueryAsync(GetAllAlarmInput input)
        {
            return await GetAlarmFilteredQuery(input);
        }
        protected override async Task<IQueryable<Alarm>> CreateBriefFilteredQueryAsync(GetAllAlarmInput input)
        {
            return await GetAlarmFilteredQuery(input);
        }

        public async Task<IQueryable<Alarm>> GetAlarmFilteredQuery(GetAllAlarmInput input)
        {
            var query = (await Repository.GetQueryableAsync())
                .WhereIf(input.UserId.HasValue, c => c.UserId == input.UserId)
                .WhereIf(!string.IsNullOrEmpty(input.Status), c => c.Status == input.Status)
                .WhereIf(!string.IsNullOrEmpty(input.Type), c => c.Type == input.Type)
                .WhereIf(input.StartDate.HasValue, c => c.CreationTime >= input.StartDate)
                .WhereIf(input.EndDate.HasValue, c => c.CreationTime <= input.EndDate);
            query = await DefaultConvention(input, query);

            return query;

        }

        protected override async Task<IEnumerable<Guid>> GetUserIdsByOrganizationAsync(Guid organizationUnitId)
        {
            var organizationUnitUsers = await organizationUnitAppService.GetOrganizationUnitUsersAsync(new GetOrganizationUnitUsersInput()
            {
                Id = organizationUnitId
            });

            var ids = organizationUnitUsers.Select(c => c.Id);
            return ids;
        }


        protected override async Task<IEnumerable<Guid>> GetUserIdsByRelatedToAsync(Guid userId, string relationType)
        {
            var ids = await relationAppService.GetRelatedToUserIdsAsync(new GetRelatedUsersInput()
            {
                UserId = userId,
                Type = relationType
            });
            return ids;

        }

    }
}
