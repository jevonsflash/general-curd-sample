using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matoapp.Identity.OrganizationUnit.Dto
{
    public class GetRootOrganizationUnitsByParentInput
    {
        public Guid? ParentId { get; set; }
        public string ParentName { get; set; }
    }
}
