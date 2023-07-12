using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Matoapp.Common.EntityFrameworkCore;

[ConnectionStringName(CommonDbProperties.ConnectionStringName)]
public class CommonDbContext : AbpDbContext<CommonDbContext>, ICommonDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
    public DbSet<DataEnum.DataEnum> DataEnum { get; set; }
    public DbSet<DataEnumCategory.DataEnumCategory> DataEnumCategory { get; set; }
    public DbSet<Tag.Tag> Tag { get; set; }


    public CommonDbContext(DbContextOptions<CommonDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureCommon();
    }
}
