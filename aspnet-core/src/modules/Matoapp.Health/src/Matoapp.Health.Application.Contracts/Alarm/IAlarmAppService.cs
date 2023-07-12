using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Application.Share.Services;
using Matoapp.Health.Alarm.Dto;

namespace Matoapp.Health.Alarm
{
    public interface IAlarmAppService : IExtendedCurdAppService<AlarmDto, AlarmDto, AlarmBriefDto, long, GetAllAlarmInput, GetAllAlarmInput,  CreateAlarmInput, UpdateAlarmInput>, IApplicationService
    {
    }
}
