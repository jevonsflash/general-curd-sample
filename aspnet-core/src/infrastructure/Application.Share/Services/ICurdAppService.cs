using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Application.Share.Services
{

    public interface ICurdAppService<TEntityDto, in TKey>
        : ICurdAppService<TEntityDto, TKey, PagedAndSortedResultRequestDto>
    {

    }

    public interface ICurdAppService<TEntityDto, in TKey, in TGetListInput>
        : ICurdAppService<TEntityDto, TKey, TGetListInput, TEntityDto>
    {

    }

    public interface ICurdAppService<TEntityDto, in TKey, in TGetListInput, in TCreateInput>
        : ICurdAppService<TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput>
    {

    }


    public interface ICurdAppService<TEntityDto, in TKey, in TGetListInput, in TCreateInput, in TUpdateInput>
        : ICurdAppService<TEntityDto, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    {

    }


    public interface ICurdAppService<TGetOutputDto, TGetListOutputDto, in TKey, in TGetListInput, in TCreateInput, in TUpdateInput>
: ICurdAppService<TGetOutputDto,  TGetListOutputDto, TKey, TGetListInput, TGetListInput, TCreateInput, TUpdateInput>
    {

    }



    public interface ICurdAppService<TGetOutputDto, TGetListOutputDto, in TKey, in TGetListInput, in TGetListBriefInput, in TCreateInput, in TUpdateInput>
    : ICurdAppService<TGetOutputDto, TGetListOutputDto, TGetListOutputDto, TKey, TGetListInput, TGetListBriefInput, TCreateInput, TUpdateInput>
    {

    }



    public interface ICurdAppService<TGetOutputDto, TGetListOutputDto, TGetListBriefOutputDto, in TKey, in TGetListInput, in TGetListBriefInput, in TCreateInput, in TUpdateInput>

    {
        Task<TGetOutputDto> GetAsync(TKey id);

        Task<PagedResultDto<TGetListOutputDto>> GetAllAsync(TGetListInput input);

        Task<TGetOutputDto> CreateAsync(TCreateInput input);

        Task<TGetOutputDto> UpdateAsync(TUpdateInput input);

        Task DeleteAsync(TKey id);

        Task<PagedResultDto<TGetListBriefOutputDto>> GetAllBriefAsync(TGetListInput input);


    }



}