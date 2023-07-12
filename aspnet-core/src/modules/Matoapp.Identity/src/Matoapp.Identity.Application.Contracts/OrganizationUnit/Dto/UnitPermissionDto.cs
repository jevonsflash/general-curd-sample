using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matoapp.Identity.OrganizationUnit.Dto
{
    public class UnitPermissionDto
    {
        public long Id { get; set; }

        public string DisplayName { get; set; }

        public long ParentId { get; set; }
    }
}
