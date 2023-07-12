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

    public abstract class CurdController<ITAppService, TEntityDto, TKey>
          : CurdController<ITAppService, TEntityDto, TKey, PagedAndSortedResultRequestDto>
          where ITAppService : ICurdAppService<TEntityDto, TKey>
          where TEntityDto : IEntityDto<TKey>
    {
        protected CurdController(ITAppService appService)
            : base(appService)
        {

        }
    }

    public abstract class CurdController<ITAppService, TEntityDto, TKey, TGetListInput>
        : CurdController<ITAppService, TEntityDto, TKey, TGetListInput, TEntityDto>
        where ITAppService : ICurdAppService<TEntityDto, TKey, TGetListInput>
        where TEntityDto : IEntityDto<TKey>
    {
        protected CurdController(ITAppService appService)
            : base(appService)
        {

        }
    }


    public abstract class CurdController<ITAppService, TEntityDto, TKey, TGetListInput, TCreateInput>
     : CurdController<ITAppService, TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput>
     where ITAppService : ICurdAppService<TEntityDto, TKey, TGetListInput, TCreateInput>
     where TEntityDto : IEntityDto<TKey>
    {
        protected CurdController(ITAppService appService)
            : base(appService)
        {

        }
    }



    public abstract class CurdController<ITAppService, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    : CurdController<ITAppService, TEntityDto, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    where ITAppService : ICurdAppService<TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
     where TEntityDto : IEntityDto<TKey>
    {
        protected CurdController(ITAppService appService)
            : base(appService)
        {

        }

    }




    public abstract class CurdController<ITAppService, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    : CurdController<ITAppService, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TGetListInput, TCreateInput, TUpdateInput>
    where ITAppService : ICurdAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    where TGetOutputDto : IEntityDto<TKey>
    where TGetListOutputDto : IEntityDto<TKey>
    {
        protected CurdController(ITAppService appService)
            : base(appService)
        {

        }

    }



    public abstract class CurdController<ITAppService, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TGetListBriefInput, TCreateInput, TUpdateInput>
    : CurdController<ITAppService, TGetOutputDto, TGetListOutputDto, TGetListOutputDto, TKey, TGetListInput, TGetListBriefInput, TCreateInput, TUpdateInput>
    where ITAppService : ICurdAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TGetListBriefInput, TCreateInput, TUpdateInput>
    where TGetOutputDto : IEntityDto<TKey>
    where TGetListOutputDto : IEntityDto<TKey>
    where TGetListBriefInput : TGetListInput
    {
        protected CurdController(ITAppService appService)
            : base(appService)
        {

        }

    }


    public abstract class CurdController<ITAppService, TGetOutputDto, TGetListOutputDto, TGetListBriefOutputDto, TKey, TGetListInput, TGetListBriefInput, TCreateInput, TUpdateInput>
        : AbpControllerBase
    where ITAppService : ICurdAppService<TGetOutputDto, TGetListOutputDto, TGetListBriefOutputDto, TKey, TGetListInput, TGetListBriefInput, TCreateInput, TUpdateInput>
    where TGetOutputDto : IEntityDto<TKey>
    where TGetListOutputDto : IEntityDto<TKey>
    where TGetListBriefInput : TGetListInput
    {

        private readonly ITAppService _recipeAppService;

        public CurdController(ITAppService recipeAppService)
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
    }
}