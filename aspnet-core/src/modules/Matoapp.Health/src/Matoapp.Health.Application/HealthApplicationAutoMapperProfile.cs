using AutoMapper;
using Matoapp.Health.Alarm.Dto;
using Matoapp.Health.Client.Dto;
using Matoapp.Health.Employee.Dto;
using Matoapp.Health.Record;
using Matoapp.Health.Record.Dto;
using Volo.Abp.AutoMapper;

namespace Matoapp.Health;

public class HealthApplicationAutoMapperProfile : Profile
{
    public HealthApplicationAutoMapperProfile()
    {
        CreateMap<Client.Client, ClientDto>().Ignore(c => c.EntityVersion);
        CreateMap<Employee.Employee, EmployeeDto>().Ignore(c => c.EntityVersion);

        CreateMap<ClientDto, Client.Client>();
        CreateMap<EmployeeDto, Employee.Employee>();

        CreateMap<Alarm.Alarm, AlarmDto>();
        CreateMap<Alarm.Alarm, AlarmBriefDto>();

        CreateMap<AlarmDto, Alarm.Alarm>().Ignore(c => c.TenantId)
                .Ignore(c => c.ConcurrencyStamp);
        CreateMap<CreateAlarmInput, Alarm.Alarm>().IgnoreFullAuditedObjectProperties()
                .IgnoreSoftDeleteProperties()
                .Ignore(c => c.TenantId)
                .Ignore(c => c.User)
                .Ignore(c => c.ConcurrencyStamp)
                .Ignore(c => c.Id);

        CreateMap<UpdateAlarmInput, Alarm.Alarm>().IgnoreFullAuditedObjectProperties()
              .IgnoreSoftDeleteProperties()
              .Ignore(c => c.TenantId)
              .Ignore(c => c.User)
              .Ignore(c => c.ConcurrencyStamp);

       

        CreateMap<SimpleValueRecord, SimpleValueRecordBriefDto>();
        CreateMap<SimpleValueRecord, SimpleValueRecordDto>();
        CreateMap<SimpleValueRecordDto, SimpleValueRecord>().Ignore(c => c.TenantId)
            .Ignore(c => c.Alarm)
                .Ignore(c => c.ConcurrencyStamp);

    
        CreateMap<CreateClientInput, Client.Client>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null));

        CreateMap<CreateClientWithUserInput, Client.Client>()
            .IgnoreFullAuditedObjectProperties()
            .IgnoreSoftDeleteProperties()
            .Ignore(c => c.LockoutEnabled)
            .Ignore(c => c.LockoutEnd)
            .Ignore(c => c.TenantId)
            .Ignore(c => c.ConcurrencyStamp)
            .Ignore(c => c.EmailConfirmed)
            .Ignore(c => c.PhoneNumberConfirmed)

            .Ignore(c => c.Id)
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null));


        CreateMap<CreateEmployeeInput, Employee.Employee>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null));

        CreateMap<CreateEmployeeWithUserInput, Employee.Employee>()

            .IgnoreFullAuditedObjectProperties()
            .IgnoreSoftDeleteProperties()
            .Ignore(c => c.LockoutEnabled)
            .Ignore(c => c.LockoutEnd)
            .Ignore(c => c.TenantId)
            .Ignore(c => c.ConcurrencyStamp)
            .Ignore(c => c.EmailConfirmed)
            .Ignore(c => c.PhoneNumberConfirmed)

            .Ignore(c => c.Id)
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null));
    }
}
