using System;
using System.Threading.Tasks;
using Domain.Share.Web.Models;
using Matoapp.Health.Client.Dto;
using HttpApi.Share.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Matoapp.Health.Client;

[Area(HealthRemoteServiceConsts.ModuleName)]
[RemoteService(Name = HealthRemoteServiceConsts.RemoteServiceName)]
[Route("api/Health/client")]
public class ClientController : CurdController<IClientAppService, ClientDto, Guid, GetAllClientInput, CreateClientInput>, IClientAppService
{
    private readonly IClientAppService _clientAppService;

    public ClientController(IClientAppService clientAppService) : base(clientAppService)
    {
        _clientAppService = clientAppService;
    }

    [HttpPost]
    [Route("CreateWithUser")]

    public async Task<ClientDto> CreateWithUserAsync(CreateClientWithUserInput input)
    {
        return await _clientAppService.CreateWithUserAsync(input);
    }

    [HttpPut]
    [Route("UpdateWithUser")]

    public Task<ClientDto> UpdateWithUserAsync(CreateClientInput input)
    {
        return _clientAppService.UpdateWithUserAsync(input);
    }
}
