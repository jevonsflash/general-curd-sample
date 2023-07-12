using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Matoapp.Common.EntityFrameworkCore;

public static class CommonDbContextModelCreatingExtensions
{
    public static void ConfigureCommon(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(CommonDbProperties.DbTablePrefix + "Questions", CommonDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */

        builder.Entity<Tag.Tag>(b =>
        {
            b.ToTable(CommonDbProperties.DbTablePrefix + nameof(Tag.Tag), CommonDbProperties.DbSchema);

            b.ConfigureByConvention();

        });
    }
}
