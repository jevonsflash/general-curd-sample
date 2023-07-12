using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Matoapp.Health.Employee
{
    public interface IEmployeeRepository : IBasicRepository<Employee, Guid>, IUserRepository<Employee>
    {
        Task<List<Employee>> GetUsersAsync(int maxCount, string filter, CancellationToken cancellationToken = default);
    }
}