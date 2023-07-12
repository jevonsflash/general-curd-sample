using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Application.Share.Dto
{
    public class CloneEntityInput<T> where T : EntityDto<long>
    {
        public List<T> TemplateDtos { get; set; }
        public long DependentEntityId { get; set; }
    }

}