using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Matoapp.Identity.EntityFrameworkCore;

public static class IdentityDbContextModelCreatingExtensions
{
    public static void ConfigureMatoIdentity(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(IdentityDbProperties.DbTablePrefix + "Questions", IdentityDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
        builder.Entity<Relation.Relation>(b =>
        {
            b.ToTable(IdentityDbProperties.DbTablePrefix + nameof(Relation.Relation), IdentityDbProperties.DbSchema);
            b.ConfigureByConvention();

            b.ApplyObjectExtensionMappings();

           
        });
        var cascadeFKs = builder.Model.FindEntityTypes(typeof(Relation.Relation))
                                .SelectMany(t => t.GetForeignKeys())
                                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
        foreach (var fk in cascadeFKs)
        {
            Console.WriteLine($"已修改{fk.GetConstraintName()}的级联删除为Restrict");
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
