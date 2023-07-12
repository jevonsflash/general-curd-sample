using System.Collections.Generic;
using Matoapp.Common.Application.DataEnums.Dtos;
using Matoapp.Common.Category.Dto;

namespace Matoapp.Common.Application.DataEnumCategory.Dto
{
    public class DataEnumCategoryDto : CategoryDto
    {
        public ICollection<DataEnumBriefDto> DataEnums { get; set; }

        public virtual ICollection<DataEnumCategoryDto> Children { get; set; }

    }
}
