using Matoapp.Health.User;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Users;

namespace Matoapp.Health.Employee.Dto
{
    public class EmployeeDto : HealthUserDto
    {

        //unique

        [StringLength(12)]
        public string EmployeeNumber { get; set; }

        [StringLength(64)]
        public string EmployeeTitle { get; set; }

        public string Introduction { get; set; }

    }
}
