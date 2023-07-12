using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Application.Share.Services
{

    public interface ISimpleCurdAppService<TEntityDto, in TKey>
        : ISimpleCurdAppService<TEntityDto, TKey, PagedAndSortedResultRequestDto>
    {

    }

    public interface ISimpleCurdAppService<TEntityDto, in TKey, in TGetListInput>
        : ISimpleCurdAppService<TEntityDto, TKey, TGetListInput, TEntityDto>
    {

    }

    public interface ISimpleCurdAppService<TEntityDto, in TKey, in TGetListInput, in TCreateInput>
        : ISimpleCurdAppService<TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput>
    {

    }

    public interface ISimpleCurdAppService<TEntityDto, in TKey, in TGetListInput, in TCreateInput, in TUpdateInput>
        : ISimpleCurdAppService<TEntityDto, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    {

    }

    public interface ISimpleCurdAppService<TGetOutputDto, TGetListOutputDto, in TKey, in TGetListInput, in TCreateInput, in TUpdateInput>

    {
        Task<TGetOutputDto> GetAsync(TKey id);

        Task<PagedResultDto<TGetListOutputDto>> GetAllAsync(TGetListInput input);

        Task<TGetOutputDto> CreateAsync(TCreateInput input);

        Task<TGetOutputDto> UpdateAsync(TUpdateInput input);

        Task DeleteAsync(TKey id);

    }
}