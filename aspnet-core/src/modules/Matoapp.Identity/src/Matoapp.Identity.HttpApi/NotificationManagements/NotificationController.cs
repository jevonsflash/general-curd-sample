using System.Collections.Generic;
using System.Threading.Tasks;
using Matoapp.Identity.NotificationManagements.Notifications;
using Matoapp.Identity.NotificationManagements.Notifications.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Domain.Share.Web.Models;

namespace Matoapp.Identity.NotificationManagements;

[Area(IdentityRemoteServiceConsts.ModuleName)]
[RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
[Route("api/Identity/notification")]
public class NotificationController : IdentityController, INotificationAppService
{
    private readonly INotificationAppService notificationAppService;

    public NotificationController(INotificationAppService notificationAppService)
    {
        this.notificationAppService = notificationAppService;
    }

    [HttpPost]
    [Route("Create")]

    public async Task CreateAsync(CreateNotificationInput input)
    {
        await notificationAppService.CreateAsync(input);
    }

    [HttpGet]
    [Route("GetBroadCastNotificationByUserId")]

    public async Task<PagedResultDto<NotificationDto>> GetBroadCastNotificationByUserIdAsync(NotificationListInput listInput)
    {
        return await notificationAppService.GetBroadCastNotificationByUserIdAsync(listInput);
    }

    [HttpGet]
    [Route("GetTextNotificationByUserId")]

    public async Task<PagedResultDto<NotificationDto>> GetTextNotificationByUserIdAsync(NotificationListInput listInput)
    {
        return await notificationAppService.GetTextNotificationByUserIdAsync(listInput);
    }

    [HttpPost]
    [Route("MarkRead")]

    public async Task MarkReadAsync(MarkReadInput input)
    {
        await notificationAppService.MarkReadAsync(input);
    }



    [HttpPost]
    [Route("SendMessageToAllClient")]

    public async Task SendMessageToAllClientAsync(SendNotificationDto sendNotificationDto)
    {
        await notificationAppService.SendMessageToAllClientAsync(sendNotificationDto);
    }

    [HttpPost]
    [Route("SendMessageToClientByUserId")]

    public async Task SendMessageToClientByUserIdAsync(SendNotificationToUserDto sendNotificationToUserDto)
    {
        await notificationAppService.SendMessageToClientByUserIdAsync(sendNotificationToUserDto);
    }
}
