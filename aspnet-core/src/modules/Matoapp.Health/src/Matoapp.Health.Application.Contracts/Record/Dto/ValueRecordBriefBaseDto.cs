using Volo.Abp.Application.Dtos;
using System;

namespace Matoapp.Health.Record.Dto
{
    public abstract class ValueRecordBriefBaseDto : FullAuditedEntityDto<long>
    {

        public virtual Guid ClientId { get; set; }

        public virtual string ClientName { get; set; }


        public virtual string Source { get; set; }

        public virtual long SourceForeId { get; set; }

        public virtual string Category { get; set; }

        public virtual string CategoryGroup { get; set; }

    }
}
