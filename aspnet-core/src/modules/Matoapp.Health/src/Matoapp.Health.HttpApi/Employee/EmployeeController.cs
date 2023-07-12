using Volo.Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Matoapp.Health.Employee.Dto;
using Application.Share.ServiceBase;
using Volo.Abp.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Volo.Abp;
using System;
using HttpApi.Share.Controller;
using Domain.Share.Web.Models;

namespace Matoapp.Health.Employee;

[Area(HealthRemoteServiceConsts.ModuleName)]
[RemoteService(Name = HealthRemoteServiceConsts.RemoteServiceName)]
[Route("api/Health/employee")]
public class EmployeeController : CurdController<IEmployeeAppService, EmployeeDto, Guid, GetAllEmployeeInput, CreateEmployeeInput>, IEmployeeAppService
{
    private readonly IEmployeeAppService _employeeAppService;

    public EmployeeController(IEmployeeAppService employeeAppService) : base(employeeAppService)
    {
        _employeeAppService = employeeAppService;
    }

    [HttpPost]
    [Route("CreateWithUser")]

    public Task<EmployeeDto> CreateWithUserAsync(CreateEmployeeWithUserInput input)
    {
        return _employeeAppService.CreateWithUserAsync(input);
    }

    [HttpPut]
    [Route("UpdateWithUser")]

    public Task<EmployeeDto> UpdateWithUserAsync(CreateEmployeeInput input)
    {
        return _employeeAppService.UpdateWithUserAsync(input);
    }
}
