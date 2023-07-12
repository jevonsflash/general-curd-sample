using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System;

namespace Matoapp.Health.Alarm.Dto
{
    public class AlarmBriefDto : FullAuditedEntityDto<long>
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int Level { get; set; }
    }
}
