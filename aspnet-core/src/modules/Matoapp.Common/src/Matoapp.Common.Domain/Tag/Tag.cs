using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Matoapp.Common.Tag
{
    public class Tag : AuditedEntity<long>, IMultiTenant
    {
        public Guid? TenantId { get; set; }


        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override long Id { get; protected set; }
        public string Title { get; set; }

        public string ForeId { get; set; }
        public int Level { get; set; }

        public int Order { get; set; }

        public string Style { get; set; }

    }
}
