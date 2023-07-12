using Matoapp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Matoapp.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MatoappEntityFrameworkCoreModule),
    typeof(MatoappApplicationContractsModule)
    )]
public class MatoappDbMigratorModule : AbpModule
{

}
