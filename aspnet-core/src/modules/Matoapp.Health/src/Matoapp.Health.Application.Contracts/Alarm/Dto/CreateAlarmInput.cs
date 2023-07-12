using Volo.Abp.Application.Dtos;
using System;
using System.Collections.Generic;
using Volo.Abp.ObjectExtending;

namespace Matoapp.Health.Alarm.Dto
{
    public class CreateAlarmInput : ExtensibleObject
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int Level { get; set; }
    }
}
