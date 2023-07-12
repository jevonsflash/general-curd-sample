using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;


namespace Matoapp.Health.Record
{
    public class SimpleValueRecord : ValueRecordBase
    {

        
        public string Unit { get; set; }

        
        public string Display { get; set; }

        
        public string UpperLimitValue { get; set; }

        
        public string UnderLimitValue { get; set; }

        
        public string ChartProperties { get; set; }

    }

}
