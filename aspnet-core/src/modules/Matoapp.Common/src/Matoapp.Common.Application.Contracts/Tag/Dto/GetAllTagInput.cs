using Volo.Abp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Share.Services;

namespace Matoapp.Common.Tag.Dto
{
    public class GetAllTagInput : PagedAndSortedResultRequestDto
    {
        public string ForeId { get; set; }
        public int? Level { get; set; }
    }
}
