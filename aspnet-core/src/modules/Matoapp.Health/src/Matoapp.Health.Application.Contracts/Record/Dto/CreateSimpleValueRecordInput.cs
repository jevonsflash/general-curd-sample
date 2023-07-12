using System;
using System.Collections.Generic;

namespace Matoapp.Health.Record.Dto
{
    public class CreateSimpleValueRecordInput
    {
        public List<ValueItem> Values { get; set; }
        public Guid ClientId { get; set; }
        public string Source { get; set; }
        public long SourceForeId { get; set; }
    }
}