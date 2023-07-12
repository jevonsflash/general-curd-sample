using System;
using System.ComponentModel.DataAnnotations;

namespace Matoapp.Identity.OrganizationUnit.Dto
{
    public class UserToOrganizationUnitInput
    {
        public Guid UserId { get; set; }

        public Guid OrganizationUnitId { get; set; }
    }
}