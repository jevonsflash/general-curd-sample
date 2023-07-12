using Volo.Abp.Application.Dtos;

namespace Matoapp.Common.Application.DataEnums.Dtos
{
    public class GetAllDataEnumInput : PagedAndSortedResultRequestDto
    {
        public string CategoryTitle { get; set; }


        public long? CategoryId { get; set; }
    }
}
