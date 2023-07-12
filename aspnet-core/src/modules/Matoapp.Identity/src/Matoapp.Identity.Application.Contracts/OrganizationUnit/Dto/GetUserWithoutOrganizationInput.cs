using Application.Share.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Matoapp.Identity.OrganizationUnit.Dto
{
    public class GetUserWithoutOrganizationInput : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public string[] Type { get; set; }
    }
}
