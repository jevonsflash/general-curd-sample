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

    public abstract class BasicCurdController<ITAppService, TEntityDto, TKey>
          : BasicCurdController<ITAppService, TEntityDto, TKey, TEntityDto>
          where ITAppService : IBasicCurdAppService<TEntityDto, TKey>
          where TEntityDto : IEntityDto<TKey>
    {
        protected BasicCurdController(ITAppService appService)
            : base(appService)
        {

        }
    }

    public abstract class BasicCurdController<ITAppService, TEntityDto, TKey, TCreateInput>
        : BasicCurdController<ITAppService, TEntityDto, TKey, TCreateInput, TCreateInput>
        where ITAppService : IBasicCurdAppService<TEntityDto, TKey, TCreateInput>
        where TEntityDto : IEntityDto<TKey>
    {
        protected BasicCurdController(ITAppService appService)
        : base(appService)
        {

        }
    }


    public abstract class BasicCurdController<ITAppService, TEntityDto, TKey, TCreateInput, TUpdateInput>
             : AbpControllerBase
             where ITAppService : IBasicCurdAppService<TEntityDto, TKey, TCreateInput, TUpdateInput>
             where TEntityDto : IEntityDto<TKey>
    {




        private readonly ITAppService _recipeAppService;

        public BasicCurdController(ITAppService recipeAppService)
        {
            _recipeAppService = recipeAppService;
        }

        [HttpPost]
        [Route("Create")]
        
        public virtual async Task<TEntityDto> CreateAsync(TCreateInput input)
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
        [Route("Get")]
        
        public virtual async Task<TEntityDto> GetAsync(TKey id)
        {
            return await _recipeAppService.GetAsync(id);
        }

        [HttpPut]
        [Route("Update")]
        
        public virtual async Task<TEntityDto> UpdateAsync(TUpdateInput input)
        {
            return await _recipeAppService.UpdateAsync(input);
        }


    }
}