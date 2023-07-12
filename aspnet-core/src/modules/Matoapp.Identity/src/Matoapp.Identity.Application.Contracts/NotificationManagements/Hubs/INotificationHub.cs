using System.Threading.Tasks;
using Matoapp.Identity.NotificationManagements.Notifications.Dtos;

namespace Matoapp.Identity.NotificationManagements.Hubs
{
    /// <summary>
    /// 
    /// </summary>
    public interface INotificationHub
    {
        /// <summary>
        /// 发送普通消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ReceiveTextMessageAsync(SendNotificationDto input);

        /// <summary>
        /// 发送广播消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ReceiveBroadCastMessageAsync(SendNotificationDto input);
    }
}