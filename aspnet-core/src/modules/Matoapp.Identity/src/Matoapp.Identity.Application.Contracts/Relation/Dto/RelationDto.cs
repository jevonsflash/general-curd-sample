using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace Matoapp.Identity.Relation.Dto
{
    public class RelationDto : ExtensibleFullAuditedEntityDto<long>
    {
        public Guid UserId { get; set; }
        public IdentityUserDto User { get; set; }

        public Guid RelatedUserId { get; set; }
        public IdentityUserDto RelatedUser { get; set; }

        public string Type { get; set; }

    }
}
