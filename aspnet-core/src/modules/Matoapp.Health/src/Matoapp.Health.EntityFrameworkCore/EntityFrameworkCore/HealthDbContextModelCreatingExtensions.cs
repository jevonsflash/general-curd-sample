using Matoapp.Health.Record;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Users.EntityFrameworkCore;

namespace Matoapp.Health.EntityFrameworkCore;

public static class HealthDbContextModelCreatingExtensions
{
    public static void ConfigureHealth(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));


        builder.Entity<Client.Client>(b =>
        {
            b.ToTable(HealthDbProperties.DbTablePrefix + nameof(Client.Client), HealthDbProperties.DbSchema);

            b.ConfigureByConvention();
            b.ConfigureAbpUser();

            b.ApplyObjectExtensionMappings();
        });

     
        builder.Entity<Employee.Employee>(b =>
        {
            b.ToTable(HealthDbProperties.DbTablePrefix + nameof(Employee.Employee), HealthDbProperties.DbSchema);

            b.ConfigureByConvention();
            b.ConfigureAbpUser();

            b.ApplyObjectExtensionMappings();
        });


        builder.Entity<Alarm.Alarm>(b =>
        {
            b.ToTable(HealthDbProperties.DbTablePrefix + nameof(Alarm.Alarm), HealthDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.ApplyObjectExtensionMappings();
        });



        builder.Entity<SimpleValueRecord>(b =>
        {
            b.ToTable(HealthDbProperties.DbTablePrefix + nameof(SimpleValueRecord), HealthDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.ApplyObjectExtensionMappings();
        });

    
     

     
        var cascadeFKs = builder.Model.GetEntityTypes()
                                 .SelectMany(t => t.GetForeignKeys())
                                 .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
        foreach (var fk in cascadeFKs)
        {
            Console.WriteLine($"已修改{fk.GetConstraintName()}的级联删除为Restrict");
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        }

    }
}
