using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System;
using Matoapp.Health.Client.Dto;

namespace Matoapp.Health.Alarm.Dto
{

    public class AlarmDto : ExtensibleFullAuditedEntityDto<long>
    {
        public Guid UserId { get; set; }

        public ClientDto User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int Level { get; set; }

    }
}
