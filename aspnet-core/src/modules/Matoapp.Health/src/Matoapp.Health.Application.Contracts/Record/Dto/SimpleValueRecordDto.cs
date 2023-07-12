namespace Matoapp.Health.Record.Dto
{
    public class SimpleValueRecordDto : ValueRecordBaseDto
    {

        public string Unit { get; set; }

        public string Display { get; set; }

        public string UpperLimitValue { get; set; }

        public string UnderLimitValue { get; set; }

        public string ChartProperties { get; set; }

    }

}
