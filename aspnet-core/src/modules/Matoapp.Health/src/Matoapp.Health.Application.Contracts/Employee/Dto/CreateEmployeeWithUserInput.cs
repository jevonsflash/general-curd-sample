using System;
using System.ComponentModel.DataAnnotations;
using Matoapp.Health.User;
using Volo.Abp.Identity;

namespace Matoapp.Health.Employee.Dto
{
    public class CreateEmployeeWithUserInput : CreateHealthUserInput
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
