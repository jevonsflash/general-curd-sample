using AutoMapper;
using Matoapp.Identity.NotificationManagements.Notifications.Aggregates;
using Matoapp.Identity.NotificationManagements.Notifications.Etos;

namespace Matoapp.Identity.NotificationManagements.Notifications
{
    public class NotificationDomainAutoMapperProfile : Profile
    {
        public NotificationDomainAutoMapperProfile()
        {
            CreateMap<Notification, NotificationEto>();
            CreateMap<NotificationSubscription, NotificationSubscriptionEto>();
        }
    }
}