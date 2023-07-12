using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;

namespace Matoapp.Health.Client
{
    public class ClientSynchronizer :
        IDistributedEventHandler<EntityUpdatedEto<UserEto>>,
        ITransientDependency
    {
        protected IClientRepository UserRepository { get; }
        protected IClientLookupService UserLookupService { get; }

        public ClientSynchronizer(
            IClientRepository userRepository,
            IClientLookupService userLookupService)
        {
            UserRepository = userRepository;
            UserLookupService = userLookupService;
        }

        public async Task HandleEventAsync(EntityUpdatedEto<UserEto> eventData)
        {
            var user = await UserRepository.FindAsync(eventData.Entity.Id);
            //if (user == null)
            //{
            //    user = await UserLookupService.FindByIdAsync(eventData.Entity.Id);
            //    if (user == null)
            //    {
            //        return;
            //    }
            //}
            if (user != null)
            {
                if (user.Update(eventData.Entity))
                {
                    await UserRepository.UpdateAsync(user);
                }
            }
        }
    }
}
