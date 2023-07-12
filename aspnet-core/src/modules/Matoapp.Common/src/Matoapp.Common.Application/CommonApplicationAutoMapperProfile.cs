using AutoMapper;
using Matoapp.Common.Tag.Dto;
using Volo.Abp.AutoMapper;

namespace Matoapp.Common;

public class CommonApplicationAutoMapperProfile : Profile
{
    public CommonApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Tag.Tag, TagDto>();

        CreateMap<TagDto, Tag.Tag>().Ignore(c => c.TenantId);
        CreateMap<CreateTagInput, Tag.Tag>().IgnoreAuditedObjectProperties()
                .Ignore(c => c.TenantId);

    }


}
