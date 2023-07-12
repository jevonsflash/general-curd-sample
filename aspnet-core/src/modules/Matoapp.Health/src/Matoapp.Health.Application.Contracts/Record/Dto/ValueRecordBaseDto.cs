using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matoapp.Health.Client.Dto;

namespace Matoapp.Health.Record.Dto
{
    public abstract class ValueRecordBaseDto : ExtensibleFullAuditedEntityDto<long>
    {

        public virtual Guid ClientId { get; set; }

        public virtual ClientDto Client { get; set; }

        public virtual string Source { get; set; }

        public virtual long SourceForeId { get; set; }

        public virtual string Value { get; set; }


        public virtual string ValueType { get; set; }

        public virtual string Type { get; set; }

        public virtual string Category { get; set; }

        public virtual string CategoryGroup { get; set; }

        public long? AlarmId { get; set; }


    }
}
