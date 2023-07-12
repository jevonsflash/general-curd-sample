using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Application.Share.Services;
using Volo.Abp.AspNetCore.Mvc;
using Domain.Share.Web.Models;

namespace HttpApi.Share.Controller
{

    public abstract class SimpleCurdController<ITAppService, TEntityDto, TKey>
          : SimpleCurdController<ITAppService, TEntityDto, TKey, PagedAndSortedResultRequestDto>
          where ITAppService : ISimpleCurdAppService<TEntityDto, TKey>
          where TEntityDto : IEntityDto<TKey>
    {
        protected SimpleCurdController(ITAppService appService)
            : base(appService)
        {

        }
    }

    public abstract class SimpleCurdController<ITAppService, TEntityDto, TKey, TGetListInput>
        : SimpleCurdController<ITAppService, TEntityDto, TKey, TGetListInput, TEntityDto>
        where ITAppService : ISimpleCurdAppService<TEntityDto, TKey, TGetListInput>
        where TEntityDto : IEntityDto<TKey>
    {
        protected SimpleCurdController(ITAppService appService)
            : base(appService)
        {

        }
    }


    public abstract class SimpleCurdController<ITAppService, TEntityDto, TKey, TGetListInput, TCreateInput>
     : SimpleCurdController<ITAppService, TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput>
     where ITAppService : ISimpleCurdAppService<TEntityDto, TKey, TGetListInput, TCreateInput>
     where TEntityDto : IEntityDto<TKey>
    {
        protected SimpleCurdController(ITAppService appService)
            : base(appService)
        {

        }
    }



    public abstract class SimpleCurdController<ITAppService, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    : SimpleCurdController<ITAppService, TEntityDto, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    where ITAppService : ISimpleCurdAppService<TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
     where TEntityDto : IEntityDto<TKey>
    {
        protected SimpleCurdController(ITAppService appService)
            : base(appService)
        {

        }

    }




    public abstract class SimpleCurdController<ITAppService, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    : AbpControllerBase
    where ITAppService : ISimpleCurdAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    where TGetOutputDto : IEntityDto<TKey>
    where TGetListOutputDto : IEntityDto<TKey>
    {
        

        private readonly ITAppService _recipeAppService;

        public SimpleCurdController(ITAppService recipeAppService)
        {
            _recipeAppService = recipeAppService;
        }

        [HttpPost]
        [Route("Create")]
        
        public virtual async Task<TGetOutputDto> CreateAsync(TCreateInput input)
        {
            return await _recipeAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("Delete")]
        
        public virtual async Task DeleteAsync(TKey id)
        {
            await _recipeAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("GetAll")]
        
        public virtual async Task<PagedResultDto<TGetListOutputDto>> GetAllAsync(TGetListInput input)
        {
            return await _recipeAppService.GetAllAsync(input);
        }

        [HttpGet]
        [Route("Get")]
        
        public virtual async Task<TGetOutputDto> GetAsync(TKey id)
        {
            return await _recipeAppService.GetAsync(id);
        }

        [HttpPut]
        [Route("Update")]
        
        public virtual async Task<TGetOutputDto> UpdateAsync(TUpdateInput input)
        {
            return await _recipeAppService.UpdateAsync(input);
        }

      
    }
}