using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Application.Share.ServiceBase;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using Matoapp.Identity.OrganizationUnit;
using Matoapp.Identity.OrganizationUnit.Dto;
using Volo.Abp;
using Matoapp.Health.Employee.Dto;

namespace Matoapp.Health.Employee

{
    //TEntity, TGetOutputDto, TGetListOutputDto, TGetListBriefOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput
    public class EmployeeAppService : CurdAppServiceBase<Employee, EmployeeDto, Guid, GetAllEmployeeInput, CreateEmployeeInput>, IEmployeeAppService
    {
        private readonly IOrganizationUnitAppService organizationUnitAppService;
        private readonly IEmployeeLookupService userLookupService;
        private readonly IIdentityUserAppService identityUserAppService;

        public EmployeeAppService(
            IOrganizationUnitAppService organizationUnitAppService,
            IEmployeeLookupService userLookupService,
            IIdentityUserAppService identityUserAppService,
            IRepository<Employee, Guid> basicInventoryRepository) : base(basicInventoryRepository)
        {
            this.organizationUnitAppService = organizationUnitAppService;
            this.userLookupService = userLookupService;
            this.identityUserAppService = identityUserAppService;
        }

        protected override async Task<IQueryable<Employee>> DefaultConvention(GetAllEmployeeInput input, IQueryable<Employee> query)
        {

            if (input.OrganizationUnitId.HasValue && !input.IsWithoutOrganization)
            {
                var organizationUnitUsers = await organizationUnitAppService.GetOrganizationUnitUsersAsync(new GetOrganizationUnitUsersInput()
                {
                    Id = input.OrganizationUnitId.Value
                });
                if (organizationUnitUsers.Count() > 0)
                {
                    var ids = organizationUnitUsers.Select(c => c.Id);
                    query = query.Where(t => ids.Contains(t.Id));
                }
                else
                {
                    query = query.Where(c => false);
                }
            }
            else if (input.IsWithoutOrganization)
            {
                var organizationUnitUsers = await organizationUnitAppService.GetUsersWithoutOrganizationAsync(new GetUserWithoutOrganizationInput());
                if (organizationUnitUsers.Count() > 0)
                {
                    var ids = organizationUnitUsers.Select(c => c.Id);
                    query = query.Where(t => ids.Contains(t.Id));
                }
                else
                {
                    query = query.Where(c => false);
                }
            }
            query = query.WhereIf(!string.IsNullOrEmpty(input.EmployeeTitle), c => c.EmployeeTitle == input.EmployeeTitle);
            query = ApplySearchFiltered(query, input);

            return query;
        }




        public async Task<EmployeeDto> CreateWithUserAsync(CreateEmployeeWithUserInput input)
        {

            var createdUser = await identityUserAppService.CreateAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync();
            var currentEmployee = await userLookupService.FindByIdAsync(createdUser.Id);
            ObjectMapper.Map(input, currentEmployee);
            var updatedEmployee = await Repository.UpdateAsync(currentEmployee);
            var result = ObjectMapper.Map<Employee, EmployeeDto>(updatedEmployee);

            if (input.OrganizationUnitId.HasValue)
            {
                await organizationUnitAppService.AddToOrganizationUnitAsync(
                    new UserToOrganizationUnitInput()
                    { UserId = createdUser.Id, OrganizationUnitId = input.OrganizationUnitId.Value });
            }
            return result;
        }



        public async Task<EmployeeDto> UpdateWithUserAsync(CreateEmployeeInput input)
        {

            var currentEmployee = await userLookupService.FindByIdAsync(input.Id);
            if (currentEmployee == null)
            {
                throw new UserFriendlyException("没有找到对应的用户");
            }
            ObjectMapper.Map(input, currentEmployee);
            var updatedEmployee = await Repository.UpdateAsync(currentEmployee);
            var result = ObjectMapper.Map<Employee, EmployeeDto>(updatedEmployee);

            return result;
        }
    }

}
