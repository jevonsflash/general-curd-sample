using Volo.Abp.Application.Dtos;
using System;
using System.Collections.Generic;
using System;

namespace Matoapp.Common.Tag.Dto
{
    public class CreateTagInput : EntityDto<long>
    {
        public string Title { get; set; }

        public string ForeId { get; set; }
        public int Level { get; set; }

        public int Order { get; set; }

        public string Style { get; set; }
    }
}
