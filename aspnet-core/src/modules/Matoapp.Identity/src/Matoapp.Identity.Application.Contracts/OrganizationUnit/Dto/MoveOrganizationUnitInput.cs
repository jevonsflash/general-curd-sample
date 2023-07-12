using System;
using System.ComponentModel.DataAnnotations;

namespace Matoapp.Identity.OrganizationUnit.Dto
{
    public class MoveOrganizationUnitInput
    {
        public Guid Id { get; set; }

        public Guid? NewParentId { get; set; }
    }
}