using Volo.Abp.Domain.Repositories;
using Matoapp.Health.Alarm.Consts;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Matoapp.Health.Alarm.Dto;
using Application.Share.ServiceBase;
using Volo.Abp.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Volo.Abp;
using HttpApi.Share.Controller;

namespace Matoapp.Health.Alarm;

[Area(HealthRemoteServiceConsts.ModuleName)]
[RemoteService(Name = HealthRemoteServiceConsts.RemoteServiceName)]
[Route("api/Health/alarm")]
public class AlarmController : ExtendedCurdController<IAlarmAppService, AlarmDto, AlarmDto, AlarmBriefDto, long, GetAllAlarmInput, GetAllAlarmInput, CreateAlarmInput, UpdateAlarmInput>, IAlarmAppService
{
    private readonly IAlarmAppService _alarmAppService;

    public AlarmController(IAlarmAppService alarmAppService) : base(alarmAppService)
    {
        _alarmAppService = alarmAppService;
    }


}
