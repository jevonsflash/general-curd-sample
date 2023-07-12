using System;
using System.Linq;
using System.Linq.Expressions;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Application.Share.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.ObjectMapping;
using Application.Share.Services;

namespace Application.Share.ServiceBase
{
   

    public abstract class BasicCurdAppServiceBase<TEntity, TEntityDto, TKey>
        : BasicCurdAppServiceBase<TEntity, TEntityDto, TKey, TEntityDto>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {
        protected BasicCurdAppServiceBase(IRepository<TEntity, TKey> repository)
            : base(repository)
        {

        }
    }


    public abstract class BasicCurdAppServiceBase<TEntity, TEntityDto, TKey, TCreateInput>
     : BasicCurdAppServiceBase<TEntity, TEntityDto, TKey, TCreateInput, TCreateInput>
     where TEntity : class, IEntity<TKey>
     where TEntityDto : IEntityDto<TKey>
    {
        protected BasicCurdAppServiceBase(IRepository<TEntity, TKey> repository)
            : base(repository)
        {

        }
    }

   


    public abstract class BasicCurdAppServiceBase<TEntity, TEntityDto, TKey, TCreateInput, TUpdateInput>
        : CrudAppService<TEntity, TEntityDto, TEntityDto, TKey, PagedAndSortedResultRequestDto, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity<TKey>
    where TEntityDto : IEntityDto<TKey>
    {
        protected BasicCurdAppServiceBase(IRepository<TEntity, TKey> repository)
    : base(repository)
        {

        }



        public virtual async Task<TEntityDto> UpdateAsync(TUpdateInput input)
        {
            await CheckUpdatePolicyAsync();
            var entity = await GetEntityByIdAsync((input as IEntityDto<TKey>).Id);
            MapToEntity(input, entity);
            await Repository.UpdateAsync(entity, autoSave: true);
            return await MapToGetOutputDtoAsync(entity);

        }
 
    }



}