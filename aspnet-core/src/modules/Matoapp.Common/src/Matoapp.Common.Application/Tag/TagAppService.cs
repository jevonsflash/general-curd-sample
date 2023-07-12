using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Matoapp.Common.Tag.Dto;
using Application.Share.ServiceBase;
using Volo.Abp.Application.Dtos;
using System;
using Matoapp.Identity.OrganizationUnit;
using Matoapp.Identity.OrganizationUnit.Dto;
using Volo.Abp.Uow;
using Microsoft.Extensions.DependencyInjection;

namespace Matoapp.Common.Tag
{
    public class TagAppService : CurdAppServiceBase<Tag, TagDto, TagDto, long, GetAllTagInput, GetAllTagInput, CreateTagInput, CreateTagInput>, ITagAppService
    {
        private readonly IUnitOfWorkManager unitOfWorkManager;

        public TagAppService(
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<Matoapp.Common.Tag.Tag, long> basicInventoryRepository) : base(basicInventoryRepository)
        {
            this.unitOfWorkManager = unitOfWorkManager;
        }

        protected override async Task<IQueryable<Tag>> CreateFilteredQueryAsync(GetAllTagInput input)
        {
            return await GetTagFilteredQuery(input);
        }
        protected override async Task<IQueryable<Tag>> CreateBriefFilteredQueryAsync(GetAllTagInput input)
        {
            return await GetTagFilteredQuery(input);
        }

        public async Task<IQueryable<Tag>> GetTagFilteredQuery(GetAllTagInput input)
        {
            var query = (await Repository.GetQueryableAsync())
                .WhereIf(input.Level.HasValue, c => c.Level == input.Level)
                .WhereIf(!string.IsNullOrEmpty(input.ForeId), c => c.ForeId == input.ForeId);
            return query;

        }
    }
}
