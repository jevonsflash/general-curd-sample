using Matoapp.Health.Record;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Matoapp.Health.EntityFrameworkCore;

[ConnectionStringName(HealthDbProperties.ConnectionStringName)]
public class HealthDbContext : AbpDbContext<HealthDbContext>, IHealthDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public DbSet<Client.Client> Client { get; set; }
    public DbSet<Employee.Employee> Employee { get; set; }

    public DbSet<Alarm.Alarm> Alarm { get; set; }

    public DbSet<SimpleValueRecord> SimpleValueRecord { get; set; }

    public HealthDbContext(DbContextOptions<HealthDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ConfigureHealth();
    }
}
