using AutoMapper;
using Matoapp.Identity.Account;
using Matoapp.Identity.OrganizationUnit.Dto;
using Matoapp.Identity.Relation.Dto;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.AutoMapper;

namespace Matoapp.Identity;

public class IdentityApplicationAutoMapperProfile : Profile
{
    public IdentityApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Volo.Abp.Identity.OrganizationUnit, OrganizationUnitDto>()
                .Ignore(c => c.Children);
        CreateMap<ModifyRelationInput, Relation.Relation>()
            .IgnoreFullAuditedObjectProperties()
            .Ignore(c => c.TenantId)
            .Ignore(c => c.ExtraProperties)
                .Ignore(c => c.ConcurrencyStamp)
            .Ignore(c => c.Id)
            .Ignore(c => c.RelatedUser)
            .Ignore(c => c.User);

        CreateMap<RelationDto, Relation.Relation>()
                     .Ignore(c => c.TenantId)
                     .Ignore(c => c.RelatedUser)
                .Ignore(c => c.ConcurrencyStamp)
            .Ignore(c => c.User);

        CreateMap<Relation.Relation, RelationDto>();
        CreateMap<UserLoginInfo, UserLoginDto>();

    }
}
