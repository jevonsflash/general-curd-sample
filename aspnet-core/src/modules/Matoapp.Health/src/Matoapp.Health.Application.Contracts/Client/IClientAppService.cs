using Application.Share.Services;
using Matoapp.Health.Client.Dto;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Matoapp.Health.Client
{
    public interface IClientAppService : ICurdAppService<ClientDto, Guid, GetAllClientInput, CreateClientInput>, IApplicationService
    {
        Task<ClientDto> CreateWithUserAsync(CreateClientWithUserInput input);
        Task<ClientDto> UpdateWithUserAsync(CreateClientInput input);
 }

}
