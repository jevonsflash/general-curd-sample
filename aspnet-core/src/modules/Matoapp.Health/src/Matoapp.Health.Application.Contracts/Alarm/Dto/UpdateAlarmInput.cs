using Volo.Abp.Application.Dtos;
using System;
using System.Collections.Generic;
using YamlDotNet.Core.Tokens;

namespace Matoapp.Health.Alarm.Dto
{
    public class UpdateAlarmInput : CreateAlarmInput, IEntityDto<long>
    {
        public long Id { get; set; }
    }
}
