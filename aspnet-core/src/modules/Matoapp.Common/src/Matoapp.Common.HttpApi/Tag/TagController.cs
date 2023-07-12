using Volo.Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Matoapp.Common.Tag.Dto;
using Application.Share.ServiceBase;
using Volo.Abp.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Volo.Abp;
using Matoapp.Common.Tag;
using HttpApi.Share.Controller;

namespace Matoapp.Common.Tags;

[Area(CommonRemoteServiceConsts.ModuleName)]
[RemoteService(Name = CommonRemoteServiceConsts.RemoteServiceName)]
[Route("api/Common/tag")]
public class TagController : CurdController<ITagAppService, TagDto, TagDto, long, GetAllTagInput, GetAllTagInput, CreateTagInput, CreateTagInput>, ITagAppService
{
    private readonly ITagAppService _tagAppService;

    public TagController(ITagAppService tagAppService) : base(tagAppService)
    {
        _tagAppService = tagAppService;
    }


}
