using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Application.Share.Services;
using Matoapp.Common.Tag.Dto;

namespace Matoapp.Common.Tag
{
    public interface ITagAppService : ICurdAppService<TagDto, TagDto, long, GetAllTagInput, GetAllTagInput, CreateTagInput, CreateTagInput>, IApplicationService
    {
    }
}
