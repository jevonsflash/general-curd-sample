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

    public abstract class ExtendedCurdController<ITAppService, TEntityDto, TKey>
          : ExtendedCurdController<ITAppService, TEntityDto, TKey, PagedAndSortedResultRequestDto>
          where ITAppService : IExtendedCurdAppService<TEntityDto, TKey>
          where TEntityDto : IEntityDto<TKey>
    {
        protected ExtendedCurdController(ITAppService appService)
            : base(appService)
        {

        }
    }

    public abstract class ExtendedCurdController<ITAppService, TEntityDto, TKey, TGetListInput>
        : ExtendedCurdController<ITAppService, TEntityDto, TKey, TGetListInput, TEntityDto>
        where ITAppService : IExtendedCurdAppService<TEntityDto, TKey, TGetListInput>
        where TEntityDto : IEntityDto<TKey>
    {
        protected ExtendedCurdController(ITAppService appService)
            : base(appService)
        {

        }
    }


    public abstract class ExtendedCurdController<ITAppService, TEntityDto, TKey, TGetListInput, TCreateInput>
     : ExtendedCurdController<ITAppService, TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput>
     where ITAppService : IExtendedCurdAppService<TEntityDto, TKey, TGetListInput, TCreateInput>
     where TEntityDto : IEntityDto<TKey>
    {
        protected ExtendedCurdController(ITAppService appService)
            : base(appService)
        {

        }
    }



    public abstract class ExtendedCurdController<ITAppService, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    : ExtendedCurdController<ITAppService, TEntityDto, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    where ITAppService : IExtendedCurdAppService<TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
     where TEntityDto : IEntityDto<TKey>
    {
        protected ExtendedCurdController(ITAppService appService)
            : base(appService)
        {

        }

    }




    public abstract class ExtendedCurdController<ITAppService, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    : ExtendedCurdController<ITAppService, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TGetListInput, TCreateInput, TUpdateInput>
    where ITAppService : IExtendedCurdAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    where TGetOutputDto : IEntityDto<TKey>
    where TGetListOutputDto : IEntityDto<TKey>
    {
        protected ExtendedCurdController(ITAppService appService)
            : base(appService)
        {

        }

    }



    public abstract class ExtendedCurdController<ITAppService, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TGetListBriefInput, TCreateInput, TUpdateInput>
    : ExtendedCurdController<ITAppService, TGetOutputDto, TGetListOutputDto, TGetListOutputDto, TKey, TGetListInput, TGetListBriefInput, TCreateInput, TUpdateInput>
    where ITAppService : IExtendedCurdAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TGetListBriefInput, TCreateInput, TUpdateInput>
    where TGetOutputDto : IEntityDto<TKey>
    where TGetListOutputDto : IEntityDto<TKey>
    where TGetListBriefInput : TGetListInput
    {
        protected ExtendedCurdController(ITAppService appService)
            : base(appService)
        {

        }

    }


    public abstract class ExtendedCurdController<ITAppService, TGetOutputDto, TGetListOutputDto, TGetListBriefOutputDto, TKey, TGetListInput, TGetListBriefInput, TCreateInput, TUpdateInput>
        : AbpControllerBase
    where ITAppService : IExtendedCurdAppService<TGetOutputDto, TGetListOutputDto, TGetListBriefOutputDto, TKey, TGetListInput, TGetListBriefInput, TCreateInput, TUpdateInput>
    where TGetOutputDto : IEntityDto<TKey>
    where TGetListOutputDto : IEntityDto<TKey>
    where TGetListBriefInput : TGetListInput
    {

        private readonly ITAppService _recipeAppService;

        public ExtendedCurdController(ITAppService recipeAppService)
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

        [HttpGet]
        [Route("GetAllBrief")]
        
        public virtual async Task<PagedResultDto<TGetListBriefOutputDto>> GetAllBriefAsync(TGetListBriefInput input)
        {
            return await _recipeAppService.GetAllBriefAsync(input);
        }

        [HttpGet]
        [Route("GetAllBriefWithoutPage")]
        
        public virtual async Task<ListResultDto<TGetListBriefOutputDto>> GetAllBriefWithoutPageAsync(TGetListBriefInput input)
        {
            return await _recipeAppService.GetAllBriefWithoutPageAsync(input);
        }
    }
}