using System.Collections.Generic;

namespace Matoapp.Infrastructure.Dto
{
    public class ElcascaderDto
    {
        public string Value { get; set; }

        public string Name { get; set; }


        public string DisplayName { get; set; }


        public List<ElcascaderDto> Children { get; set; }
        public string GroupName { get; set; }


    }
}
