using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Application.Share.Services;
using Matoapp.Common.Application.DataEnums.Dtos;

namespace Matoapp.Common.DataEnum
{
    public interface IDataEnumAppService : ICurdAppService<DataEnumDto, DataEnumBriefDto, long, GetAllDataEnumInput, GetAllDataEnumInput, DataEnumDto, DataEnumDto>, IApplicationService
    {
    }
}
