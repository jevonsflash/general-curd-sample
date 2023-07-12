using Matoapp.Identity.NotificationManagements.Notifications.Enums;

namespace Matoapp.Identity.NotificationManagements.Notifications.Dtos
{
    /// <summary>
    /// 发送通知
    /// </summary>
    public class SendNotificationDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType MessageType { get; set; }

        private SendNotificationDto()
        {

        }

        public SendNotificationDto(string title, string content, MessageType messageType)
        {
            Title = title;
            Content = content;
            MessageType = messageType;
        }
    }
}