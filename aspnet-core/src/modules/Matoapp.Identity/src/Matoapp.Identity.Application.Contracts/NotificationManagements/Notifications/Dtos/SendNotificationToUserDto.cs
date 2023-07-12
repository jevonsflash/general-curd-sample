using Matoapp.Identity.NotificationManagements.Notifications.Enums;
using System.Collections.Generic;

namespace Matoapp.Identity.NotificationManagements.Notifications.Dtos
{
    /// <summary>
    /// ·¢ËÍÍ¨Öª
    /// </summary>
    public class SendNotificationToUserDto : SendNotificationDto
    {

        public List<string> Users { get; set; }

        public SendNotificationToUserDto(string title, string content, MessageType messageType, List<string> users) : base(title, content, messageType)
        {
            this.Users = users;
        }
    }
}