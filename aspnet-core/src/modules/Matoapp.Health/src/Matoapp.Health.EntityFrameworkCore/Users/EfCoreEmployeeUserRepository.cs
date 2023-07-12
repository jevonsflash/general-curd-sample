using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Matoapp.Health.Employee;
using Matoapp.Health.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Users.EntityFrameworkCore;

namespace Matoapp.Health.Users
{
    public class EfCoreEmployeeRepository : EfCoreUserRepositoryBase<IHealthDbContext, Health.Employee.Employee>, IEmployeeRepository
    {
        public EfCoreEmployeeRepository(IDbContextProvider<IHealthDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Health.Employee.Employee>> GetUsersAsync(int maxCount, string filter, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.UserName.Contains(filter))
                .Take(maxCount).ToListAsync(cancellationToken);
        }
    }
}
