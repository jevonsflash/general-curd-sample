using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Volo.Abp.Security.Claims;
using Volo.Abp.Uow;
using System.Security.Principal;

namespace Matoapp.Identity.Identity
{
    public class MatoappUserClaimsPrincipalFactory : AbpUserClaimsPrincipalFactory
    {
        public MatoappUserClaimsPrincipalFactory(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options, ICurrentPrincipalAccessor currentPrincipalAccessor, IAbpClaimsPrincipalFactory abpClaimsPrincipalFactory) : base(userManager, roleManager, options, currentPrincipalAccessor, abpClaimsPrincipalFactory)
        {
        }
        [UnitOfWork]
        public override async Task<ClaimsPrincipal> CreateAsync(IdentityUser user)
        {
            var principal = await base.CreateAsync(user);       
            return principal;
        }


    }
}
