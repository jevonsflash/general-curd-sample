using Application.Share.Dto;
using Matoapp.Health.User;
using System;
using Volo.Abp.Application.Dtos;

namespace Matoapp.Health.Employee.Dto
{
    public class GetAllEmployeeInput : GetAllHealthUserInput
    {
        public string EmployeeTitle { get; set; }

    }

}