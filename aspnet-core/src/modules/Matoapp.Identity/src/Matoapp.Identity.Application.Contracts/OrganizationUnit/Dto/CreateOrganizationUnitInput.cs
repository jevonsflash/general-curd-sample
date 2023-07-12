using System;
using System.ComponentModel.DataAnnotations;

namespace Matoapp.Identity.OrganizationUnit.Dto
{
    public class CreateOrganizationUnitInput
    {
        public Guid? ParentId { get; set; }

        [Required]
        public string DisplayName { get; set; }

        public string NationalCode { get; set; }
    }
}