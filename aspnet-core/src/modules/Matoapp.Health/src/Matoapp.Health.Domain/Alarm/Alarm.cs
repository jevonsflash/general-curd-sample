using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Matoapp.Health.Alarm
{
    public class Alarm : FullAuditedAggregateRoot<long>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public Client.Client User { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override long Id { get; protected set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }

        public int Level { get; set; }

    }
}
