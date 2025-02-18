﻿namespace Domain.Entities.ContractAggregate.Static
{
    public class StaticContract : SoftDeletableEntity
    {

        public string TitleAr { get; set; }
        public string TitleEn { get; set; }

        public string PreambleAr { get; set; }
        public string PreambleEn { get; set; }

        public string ClosingRemarkAr { get; set; }
        public string ClosingRemarkEn { get; set; }

    }
}
