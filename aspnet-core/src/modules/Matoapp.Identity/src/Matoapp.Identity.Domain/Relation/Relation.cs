using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;



namespace Matoapp.Identity.Relation
{

    public class Relation : FullAuditedAggregateRoot<long>
    {
        public Guid? TenantId { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override long Id { get; protected set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        public Guid RelatedUserId { get; set; }

        [ForeignKey("RelatedUserId")]
        public IdentityUser RelatedUser { get; set; }

        public string Type { get; set; }

    }


}
