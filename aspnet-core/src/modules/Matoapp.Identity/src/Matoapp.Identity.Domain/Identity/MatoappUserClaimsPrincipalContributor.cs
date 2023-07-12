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
using Volo.Abp.DependencyInjection;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;
using Volo.Abp.Users;
using Domain.Share.Utils.Extensions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Domain.Repositories;
using OpenIddict.Abstractions;

namespace Matoapp.Identity.Identity
{
    public class MatoappUserClaimsPrincipalContributor : IAbpClaimsPrincipalContributor, ITransientDependency
    {
        private readonly ICurrentUser currentUser;
        private readonly IdentityUserManager userManager;
        private readonly IUserRoleFinder userRoleFinder;
        private readonly IIdentityUserRepository userRepository;
        private readonly IdentityRoleManager roleManager;
        private readonly IPermissionGrantRepository permissionGrantRepository;
        private readonly IPermissionManager permissionManager;

        public MatoappUserClaimsPrincipalContributor(ICurrentUser currentUser,
            IdentityUserManager userManager,
            IUserRoleFinder userRoleFinder,
            IIdentityUserRepository userRepository,
            IdentityRoleManager roleManager,
        IPermissionGrantRepository permissionGrantRepository,

            IPermissionManager permissionManager)
        {
            this.currentUser = currentUser;
            this.userManager = userManager;
            this.userRoleFinder = userRoleFinder;
            this.userRepository = userRepository;
            this.roleManager = roleManager;
            this.permissionGrantRepository = permissionGrantRepository;
            this.permissionManager = permissionManager;
        }
        public async Task ContributeAsync(AbpClaimsPrincipalContributorContext context)
        {
            var identity = context.ClaimsPrincipal.Identities.FirstOrDefault();
            if (identity != null && currentUser.Id.HasValue)
            {

                var userId = currentUser.Id.Value;
                var identityUser = await userManager.FindByIdAsync(userId.ToString());
                await userRepository.EnsureCollectionLoadedAsync(identityUser, u => u.Roles);
                var profile = new MatoappUserProfileClaim();
                object avatar;
                if (identityUser.ExtraProperties.TryGetValue("AvatarUrl", out avatar))
                {
                    profile.AvatarUrl = avatar.ToString();
                }


                var permissionGrants = new List<PermissionGrant>();


                var roleNames = await userRoleFinder.GetRolesAsync(userId);

                foreach (var roleName in roleNames)
                {
                    permissionGrants.AddRange(await permissionGrantRepository.GetListAsync(RolePermissionValueProvider.ProviderName, roleName));
                }


                permissionGrants = permissionGrants.Distinct().ToList();

                profile.Role = roleNames;
                profile.Permission = permissionGrants.Select(c => c.Name).ToArray();

                identity.AddIfNotContains(new Claim(OpenIddictConstants.Permissions.Scopes.Profile, profile.ToJsonString()));;

            }

        }
    }
}
