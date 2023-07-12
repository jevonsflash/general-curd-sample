using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace Matoapp.Identity.Account
{
    [Area(IdentityRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Route("api/identity/account")]
    public class AccountController : IdentityController, IAccountAppService
    {
        private readonly IAccountAppService _accountAppService;

        public AccountController(IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }

        [HttpGet]
        [Route("FindByLogin")]
        public async Task<IdentityUserDto> FindByLoginAsync(string loginProvider, string providerKey)
        {
            return await _accountAppService.FindByLoginAsync(loginProvider, providerKey);
        }

        [HttpGet]
        [Route("GetCurrentUserLogins")]
        public async Task<IList<UserLoginDto>> GetCurrentUserLoginsAsync()
        {
            return await _accountAppService.GetCurrentUserLoginsAsync();
        }


        [HttpGet]
        [Route("GetLogins")]
        public async Task<IList<UserLoginDto>> GetLoginsAsync(Guid userId)
        {
            return await _accountAppService.GetLoginsAsync(userId);
        }
    }
}