using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Matoapp.Common.Category.Dto
{
    public class CategoryDto : FullAuditedEntityDto<long>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public int Level { get; set; }

        public int Order { get; set; }

        public string Style { get; set; }

        public long? ParentId { get; set; }



    }
}
