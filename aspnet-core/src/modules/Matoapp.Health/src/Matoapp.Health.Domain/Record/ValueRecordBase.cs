using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.MultiTenancy;

namespace Matoapp.Health.Record
{
    public abstract class ValueRecordBase : FullAuditedAggregateRoot<long>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override long Id { get; protected set; }

        public virtual Guid ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client.Client Client { get; set; }

        
        public virtual string Source { get; set; }

        
        public virtual long SourceForeId { get; set; }

        
        public virtual string Value { get; set; }


        
        public virtual string ValueType { get; set; }

        
        public virtual string Type { get; set; }

        
        public virtual string Category { get; set; }


        
        public virtual string CategoryGroup { get; set; }

        
        public long? AlarmId { get; set; }

        [ForeignKey("AlarmId")]
        public Alarm.Alarm? Alarm { get; set; }  

    }
}
