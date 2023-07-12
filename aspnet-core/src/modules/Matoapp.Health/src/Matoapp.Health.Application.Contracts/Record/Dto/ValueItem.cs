namespace Matoapp.Health.Record.Dto
{
    public class ValueItem
    {
        public string Value { get; set; }
        public string ValueType { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }

        public string CategoryGroup { get; set; }
        public string Display { get; set; }

        public string UpperLimitValue { get; set; }

        public string UnderLimitValue { get; set; }

        public string ChartProperties { get; set; }
    }
}