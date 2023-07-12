using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Application.Share.Services
{

    public interface IExtendedCurdAppService<TEntityDto, in TKey>
        : IExtendedCurdAppService<TEntityDto, TKey, PagedAndSortedResultRequestDto>
    {

    }

    public interface IExtendedCurdAppService<TEntityDto, in TKey, in TGetListInput>
        : IExtendedCurdAppService<TEntityDto, TKey, TGetListInput, TEntityDto>
    {

    }

    public interface IExtendedCurdAppService<TEntityDto, in TKey, in TGetListInput, in TCreateInput>
        : IExtendedCurdAppService<TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput>
    {

    }


    public interface IExtendedCurdAppService<TEntityDto, in TKey, in TGetListInput, in TCreateInput, in TUpdateInput>
        : IExtendedCurdAppService<TEntityDto, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    {

    }


    public interface IExtendedCurdAppService<TGetOutputDto, TGetListOutputDto, in TKey, in TGetListInput, in TCreateInput, in TUpdateInput>
: IExtendedCurdAppService<TGetOutputDto,  TGetListOutputDto, TKey, TGetListInput, TGetListInput, TCreateInput, TUpdateInput>
    {

    }



    public interface IExtendedCurdAppService<TGetOutputDto, TGetListOutputDto, in TKey, in TGetListInput, in TGetListBriefInput, in TCreateInput, in TUpdateInput>
    : IExtendedCurdAppService<TGetOutputDto, TGetListOutputDto, TGetListOutputDto, TKey, TGetListInput, TGetListBriefInput, TCreateInput, TUpdateInput>
    {

    }



    public interface IExtendedCurdAppService<TGetOutputDto, TGetListOutputDto, TGetListBriefOutputDto, in TKey, in TGetListInput, in TGetListBriefInput, in TCreateInput, in TUpdateInput>

    {
        Task<TGetOutputDto> GetAsync(TKey id);

        Task<PagedResultDto<TGetListOutputDto>> GetAllAsync(TGetListInput input);

        Task<TGetOutputDto> CreateAsync(TCreateInput input);

        Task<TGetOutputDto> UpdateAsync(TUpdateInput input);

        Task DeleteAsync(TKey id);

        Task<PagedResultDto<TGetListBriefOutputDto>> GetAllBriefAsync(TGetListInput input);
     
        
        Task<ListResultDto<TGetListBriefOutputDto>> GetAllBriefWithoutPageAsync(TGetListInput input);


    }



}