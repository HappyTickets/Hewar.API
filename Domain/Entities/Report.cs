﻿namespace Domain.Entities
{
    public class Report : SoftDeletableEntity
    {
        public string ReportType { get; set; }
        public DateTimeOffset GeneratedDate { get; set; }
        public string Data { get; set; }
        public string Filters { get; set; }
    }
}
