using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace Matoapp.Health.Client
{
    public class ClientLookupService : UserLookupService<Client, IClientRepository>, IClientLookupService
    {
        public ClientLookupService(
            IClientRepository userRepository,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
                userRepository,
                unitOfWorkManager)
        {

        }

        protected override Client CreateUser(IUserData externalUser)
        {
            return new Client(externalUser);
        }
    }
}