using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Identity;

namespace Matoapp.Identity.OrganizationUnit.Dto
{
    public class UsersGroupByOrganizationUnitDto
    {
        public string DisplayName { get; set; }

        public virtual ICollection<IdentityUserDto> Children { get; set; }
    }
}
