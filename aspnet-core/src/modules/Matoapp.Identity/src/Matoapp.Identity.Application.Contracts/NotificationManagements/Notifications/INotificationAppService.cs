using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Matoapp.Identity.NotificationManagements.Notifications.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Matoapp.Identity.NotificationManagements.Notifications
{
    public interface INotificationAppService : IApplicationService
    {

        /// <summary>
        /// 消息设置为已读
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task MarkReadAsync(MarkReadInput input);

        /// <summary>
        /// 创建一个消息
        /// 测试使用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateNotificationInput input);

        /// <summary>
        /// 分页获取用户普通文本消息
        /// </summary>
        /// <param name="listInput"></param>
        /// <returns></returns>
        Task<PagedResultDto<NotificationDto>> GetTextNotificationByUserIdAsync(NotificationListInput listInput);

        /// <summary>
        /// 分页获取广播消息
        /// </summary>
        /// <param name="listInput"></param>
        /// <returns></returns>
        Task<PagedResultDto<NotificationDto>> GetBroadCastNotificationByUserIdAsync(NotificationListInput listInput);
        Task SendMessageToAllClientAsync(SendNotificationDto sendNotificationDto);
        Task SendMessageToClientByUserIdAsync(SendNotificationToUserDto sendNotificationToUserDto);
    }
}