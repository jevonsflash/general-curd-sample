using Matoapp.Health;
using Matoapp.Health.Record;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Matoapp.Health.EntityFrameworkCore;

[ConnectionStringName(HealthDbProperties.ConnectionStringName)]
public interface IHealthDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */

    DbSet<Client.Client> Client { get; set; }
    DbSet<Employee.Employee> Employee { get; set; }

    DbSet<Alarm.Alarm> Alarm { get; set; }

    DbSet<SimpleValueRecord> SimpleValueRecord { get; set; }
}
