using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Matoapp.Health.Client
{
    public interface IClientRepository : IBasicRepository<Client, Guid>, IUserRepository<Client>
    {
        Task<List<Client>> GetUsersAsync(int maxCount, string filter, CancellationToken cancellationToken = default);
    }
}