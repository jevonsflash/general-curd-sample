using Matoapp.Identity.Identity;
using Matoapp.Identity.Relation;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Matoapp.Identity.EntityFrameworkCore;

[DependsOn(
    typeof(IdentityDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class IdentityEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<IdentityDbContext>(options =>
        {
            options.AddRepository<IdentityUserOrganizationUnit, EfCoreRepository<IdentityDbContext, IdentityUserOrganizationUnit>>();
            options.AddRepository<Relation.Relation, EfCoreRepository<IdentityDbContext, Relation.Relation>>();
        });
    }
}
