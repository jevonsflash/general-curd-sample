using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Matoapp.Identity.Identity;
using Volo.Abp.Auditing;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.PermissionManagement;

namespace Matoapp.Identity;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpIdentityDomainModule),
    typeof(IdentityDomainSharedModule),
    typeof(AbpPermissionManagementDomainModule)
)]
public class IdentityDomainModule : AbpModule
{
    //public override void PreConfigureServices(ServiceConfigurationContext context)
    //{
    //    PreConfigure<IdentityBuilder>(builder =>
    //    {
    //        builder.AddClaimsPrincipalFactory<CAHUserClaimsPrincipalFactory>();
    //    });
    //}

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<AbpIdentityDomainModule>();      
    }


}
