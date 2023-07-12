using Application.Share.Services;
using Matoapp.Health.Employee.Dto;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Matoapp.Health.Employee
{
    public interface IEmployeeAppService : ICurdAppService<EmployeeDto, Guid, GetAllEmployeeInput, CreateEmployeeInput>, IApplicationService
    {
        Task<EmployeeDto> CreateWithUserAsync(CreateEmployeeWithUserInput input);
        Task<EmployeeDto> UpdateWithUserAsync(CreateEmployeeInput input);
    }

}
