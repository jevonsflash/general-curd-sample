using Matoapp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Matoapp;

[DependsOn(
    typeof(MatoappEntityFrameworkCoreTestModule)
    )]
public class MatoappDomainTestModule : AbpModule
{

}
