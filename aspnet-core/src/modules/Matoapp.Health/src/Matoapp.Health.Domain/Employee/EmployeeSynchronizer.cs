﻿using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;

namespace Matoapp.Health.Employee
{
    public class EmployeeSynchronizer :
        IDistributedEventHandler<EntityUpdatedEto<UserEto>>,
        ITransientDependency
    {
        protected IEmployeeRepository UserRepository { get; }
        protected IEmployeeLookupService UserLookupService { get; }

        public EmployeeSynchronizer(
            IEmployeeRepository userRepository,
            IEmployeeLookupService userLookupService)
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
