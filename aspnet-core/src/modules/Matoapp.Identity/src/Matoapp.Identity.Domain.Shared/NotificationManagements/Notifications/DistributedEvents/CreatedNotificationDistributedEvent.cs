using Matoapp.Identity.NotificationManagements.Notifications.Etos;

namespace Matoapp.Identity.NotificationManagements.Notifications.DistributedEvents
{
    public class CreatedNotificationDistributedEvent
    {
        public NotificationEto NotificationEto { get; set; }

        private CreatedNotificationDistributedEvent()
        {

        }

        public CreatedNotificationDistributedEvent(NotificationEto notificationEto)
        {
            NotificationEto = notificationEto;
        }
    }
}