using System;

namespace Matoapp.Identity.NotificationManagements.Notifications.Dtos
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }
        public bool Read { get; set; }
    }
}