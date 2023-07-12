using System;
using System.ComponentModel.DataAnnotations;
using Matoapp.Health.User;

namespace Matoapp.Health.Employee.Dto
{
    public class CreateEmployeeInput : HealthUserDto
    {

        //public ICollection<TagDto> Tags { get; set; }

        public Guid? OrganizationUnitId { get; set; }
      
        [StringLength(12)]
        public string EmployeeNumber { get; set; }

        [StringLength(64)]
        public string EmployeeTitle { get; set; }

        public string Introduction { get; set; }

    }

}
