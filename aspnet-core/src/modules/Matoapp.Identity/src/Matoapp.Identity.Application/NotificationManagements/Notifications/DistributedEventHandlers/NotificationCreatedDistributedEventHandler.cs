using System.Linq;
using System.Threading.Tasks;
using Matoapp.Identity.NotificationManagements.Notifications.DistributedEvents;
using Matoapp.Identity.NotificationManagements.Notifications.Enums;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Matoapp.Identity.NotificationManagements.Notifications.DistributedEventHandlers
{
    /// <summary>
    /// 创建消息事件处理
    /// </summary>
    public class NotificationCreatedDistributedEventHandler : IDistributedEventHandler<CreatedNotificationDistributedEvent>, ITransientDependency
    {
        private readonly INotificationAppService _hubAppService;

        public NotificationCreatedDistributedEventHandler(INotificationAppService hubAppService)
        {
            _hubAppService = hubAppService;
        }

        public async Task HandleEventAsync(CreatedNotificationDistributedEvent eventData)
        {
            switch (eventData.NotificationEto.MessageType)
            {
                case MessageType.Text:
                    await _hubAppService.SendMessageToClientByUserIdAsync(new Dtos.SendNotificationToUserDto(
                eventData.NotificationEto.Title,
                eventData.NotificationEto.Content,
                eventData.NotificationEto.MessageType,
                eventData.NotificationEto.NotificationSubscriptions.Select(e => e.ReceiveId.ToString()).ToList()));
                    break;
                case MessageType.BroadCast:
                    await _hubAppService.SendMessageToAllClientAsync(new Dtos.SendNotificationDto(
                eventData.NotificationEto.Title,
                eventData.NotificationEto.Content,
                eventData.NotificationEto.MessageType));
                    break;
                default:
                    //throw new UserFriendlyException("未知的消息类型");
                    break;
            }


        }
    }
}