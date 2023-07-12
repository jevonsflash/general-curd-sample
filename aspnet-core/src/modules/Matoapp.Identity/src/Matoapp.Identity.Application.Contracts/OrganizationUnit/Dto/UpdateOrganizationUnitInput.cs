using System;
using System.ComponentModel.DataAnnotations;

namespace Matoapp.Identity.OrganizationUnit.Dto
{
    public class UpdateOrganizationUnitInput
    {
        public Guid Id { get; set; }

        [Required]
        public string DisplayName { get; set; }
    }
}