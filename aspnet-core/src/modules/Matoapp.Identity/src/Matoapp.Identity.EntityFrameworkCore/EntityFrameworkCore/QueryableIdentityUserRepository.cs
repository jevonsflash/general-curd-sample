using Matoapp.Identity.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using VoloIIdentityDbContext = Volo.Abp.Identity.EntityFrameworkCore.IIdentityDbContext;

namespace Matoapp.Identity.EntityFrameworkCore
{
    public class QueryableIdentityUserRepository : EfCoreIdentityUserRepository, IQueryableIdentityUserRepository
    {
        private readonly IDbContextProvider<VoloIIdentityDbContext> dbContextProvider;

        public QueryableIdentityUserRepository(IDbContextProvider<VoloIIdentityDbContext> dbContextProvider) : base(dbContextProvider)
        {
            this.dbContextProvider = dbContextProvider;
        }

        public virtual async Task<IQueryable<OrganizationUnit>> GetOrganizationUnitsQueryableAsync(
         Guid id,
         bool includeDetails = false)
        {
            var dbContext = await GetDbContextAsync();

            var query = from userOu in dbContext.Set<IdentityUserOrganizationUnit>()
                        join ou in dbContext.OrganizationUnits.IncludeDetails(includeDetails) on userOu.OrganizationUnitId equals ou.Id
                        where userOu.UserId == id
                        select ou;

            return query;
        }


        public virtual async Task<IQueryable<IdentityUser>> GetOrganizationUnitUsersAsync(
         Guid id, string keyword, string[] type,
         bool includeDetails = false)
        {
            var dbContext = await GetDbContextAsync();

            var query = (from user in (await GetQueryableAsync())
                        .WhereIf(!keyword.IsNullOrWhiteSpace(),
                        FilterByKeyword(keyword))
                        .WhereIf(type != null && type.Length > 0, c => type.Contains(c.ExtraProperties["Type"]))
                         join uou in dbContext.Set<IdentityUserOrganizationUnit>()
                         .Where(c => c.OrganizationUnitId == id) on user.Id equals uou.UserId
                         select user).Distinct();
            return query;
        }


        public async Task<IQueryable<IdentityUser>> GetUsersWithoutOrganizationAsync(string keyword, string[] type)
        {
            var dbContext = await GetDbContextAsync();
            //每个用户对应只能有一个地点
            var query = (from user in (await GetQueryableAsync())
                 .WhereIf(
                     !keyword.IsNullOrWhiteSpace(),
                        FilterByKeyword(keyword)
                 ).WhereIf(type != null && type.Length > 0, c => type.Contains(c.ExtraProperties["Type"]))
                         join userOrganization in dbContext.Set<IdentityUserOrganizationUnit>() on user.Id equals userOrganization.UserId into t
                         from uo in t.DefaultIfEmpty()
                         where uo == null
                         select user)
            .Distinct();
            return query;
        }




        private Expression<Func<IdentityUser, bool>> FilterByKeyword(string keyword)
        {
            return user => (user.Surname + user.Name).Contains(keyword);
        }
    }
}
