using Volo.Abp.Modularity;

namespace Matoapp;

[DependsOn(
    typeof(MatoappApplicationModule),
    typeof(MatoappDomainTestModule)
    )]
public class MatoappApplicationTestModule : AbpModule
{

}
