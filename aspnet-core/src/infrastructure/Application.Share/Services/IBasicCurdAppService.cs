using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Application.Share.Services
{


    public interface IBasicCurdAppService<TEntityDto, in TKey>
        : IBasicCurdAppService<TEntityDto, TKey,  TEntityDto>
    {

    }

    public interface IBasicCurdAppService<TEntityDto, in TKey,  in TCreateInput>
        : IBasicCurdAppService<TEntityDto, TKey,  TCreateInput, TCreateInput>
    {

    }



    public interface IBasicCurdAppService<TEntityDto,  in TKey,  in TCreateInput, in TUpdateInput>

    {
        Task<TEntityDto> GetAsync(TKey id);

        Task<TEntityDto> CreateAsync(TCreateInput input);

        Task<TEntityDto> UpdateAsync(TUpdateInput input);

        Task DeleteAsync(TKey id);

    }
}