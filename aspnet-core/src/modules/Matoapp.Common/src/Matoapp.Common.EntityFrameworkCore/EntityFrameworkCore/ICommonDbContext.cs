using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Matoapp.Common.EntityFrameworkCore;

[ConnectionStringName(CommonDbProperties.ConnectionStringName)]
public interface ICommonDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */

    DbSet<DataEnum.DataEnum> DataEnum { get; set; }
    DbSet<DataEnumCategory.DataEnumCategory> DataEnumCategory { get; set; }
    DbSet<Tag.Tag> Tag { get; set; }
}
