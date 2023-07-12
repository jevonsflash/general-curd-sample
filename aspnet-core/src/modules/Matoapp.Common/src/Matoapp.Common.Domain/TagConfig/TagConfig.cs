using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Matoapp.Common.TagConfig
{

    public class TagConfig<TEntityKey> : AuditedEntity<long>
    {
        public int? TenantId { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override long Id { get; protected set; }

        public TEntityKey EntityId { get; set; }

        public long TagId { get; set; }

        [ForeignKey("TagId")]
        public Tag.Tag Tag { get; set; }
    }

    public class TagConfigForLong : TagConfig<long> { }
    public class TagConfigForGuid : TagConfig<Guid> { }


}
