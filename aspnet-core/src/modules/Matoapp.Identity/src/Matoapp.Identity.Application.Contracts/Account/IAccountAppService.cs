using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Matoapp.Identity.Account
{
    public interface IAccountAppService
    {
        Task<IdentityUserDto> FindByLoginAsync(string loginProvider, string providerKey);
        Task<IList<UserLoginDto>> GetCurrentUserLoginsAsync();
        Task<IList<UserLoginDto>> GetLoginsAsync(Guid userId);
    }
}