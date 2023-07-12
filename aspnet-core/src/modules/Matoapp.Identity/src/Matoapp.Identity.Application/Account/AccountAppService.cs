using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Users;
using static Volo.Abp.Identity.IdentityPermissions;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace Matoapp.Identity.Account;

public class AccountAppService : IdentityAppService, IAccountAppService
{
    private readonly ICurrentUser currentUser;
    private readonly IdentityUserManager identityUserManager;

    public AccountAppService(
        ICurrentUser currentUser,
        IdentityUserManager identityUserManager)
    {
        this.currentUser = currentUser;
        this.identityUserManager = identityUserManager;
    }


    public async Task<IdentityUserDto> FindByLoginAsync(string loginProvider, string providerKey)
    {
        var user = await identityUserManager.FindByLoginAsync(loginProvider, providerKey);
        var result = ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
        return result;
    }

    public async Task<IList<UserLoginDto>> GetLoginsAsync(Guid userId)
    {
        var user = await identityUserManager.GetByIdAsync(userId);
        if (user == null)
        {
            throw new UserFriendlyException("找不到用户");
        }
        var result = await identityUserManager.GetLoginsAsync(user);
        return ObjectMapper.Map<List<UserLoginInfo>, List<UserLoginDto>>((List<UserLoginInfo>)result);

    }


    public async Task<IList<UserLoginDto>> GetCurrentUserLoginsAsync()
    {
        var currentUserId = currentUser.GetId();
        return await GetLoginsAsync(currentUserId);
    }
}
