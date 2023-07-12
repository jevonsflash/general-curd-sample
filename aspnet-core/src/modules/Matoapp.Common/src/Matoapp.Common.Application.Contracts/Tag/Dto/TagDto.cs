using Volo.Abp.Application.Dtos;
using System;

namespace Matoapp.Common.Tag.Dto
{

    public class TagDto : AuditedEntityDto<long>
    {
        public string Title { get; set; }

        public string ForeId { get; set; }
        public int Level { get; set; }

        public int Order { get; set; }

        public string Style { get; set; }
    }
}
