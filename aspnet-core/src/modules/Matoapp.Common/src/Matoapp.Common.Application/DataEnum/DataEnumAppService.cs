using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Application.Share.ServiceBase;
using Volo.Abp.Uow;
using Matoapp.Common.Application.DataEnums.Dtos;

namespace Matoapp.Common.DataEnum
{
    public class DataEnumAppService : CurdAppServiceBase<DataEnum, DataEnumDto, DataEnumBriefDto, long, GetAllDataEnumInput, DataEnumDto, DataEnumDto>, IDataEnumAppService
    {
        private readonly IUnitOfWorkManager unitOfWorkManager;

        public DataEnumAppService(
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<DataEnum, long> basicInventoryRepository) : base(basicInventoryRepository)
        {
            this.unitOfWorkManager = unitOfWorkManager;
        }

        protected override async Task<IQueryable<DataEnum>> CreateFilteredQueryAsync(GetAllDataEnumInput input)
        {
            return await GetDataEnumFilteredQuery(input);
        }
        protected override async Task<IQueryable<DataEnum>> CreateBriefFilteredQueryAsync(GetAllDataEnumInput input)
        {
            return await GetDataEnumFilteredQuery(input);
        }

        public async Task<IQueryable<DataEnum>> GetDataEnumFilteredQuery(GetAllDataEnumInput input)
        {
            var query = (await Repository.WithDetailsAsync())
                .WhereIf(!string.IsNullOrEmpty(input.CategoryTitle), c => c.DataEnumCategory.Title == input.CategoryTitle)
                .WhereIf(input?.CategoryId != null, c => c.DataEnumCategory.Id == input.CategoryId);
            return query;

        }
    }
}
