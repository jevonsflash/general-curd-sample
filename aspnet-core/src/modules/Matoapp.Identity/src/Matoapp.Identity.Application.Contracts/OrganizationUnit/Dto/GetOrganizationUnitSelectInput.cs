using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matoapp.Identity.OrganizationUnit.Dto
{

    public class GetOrganizationUnitSelectInput
    {
        public int Type { get; set; } = 15;

        public long? ParentId { get; set; }
    }
}
