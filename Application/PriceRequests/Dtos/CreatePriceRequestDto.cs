﻿using Domain.Enums;

namespace Application.PriceRequests.Dtos
{
    public class CreatePriceRequestDto
    {
        public SecurityRoles SecurityRole { get; set; }
        public int GuardsCount { get; set; }
        public WorkShifts WorkShift { get; set; }
        public ContractTypes ContractType { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Description { get; set; }
        public long CompanyId { get; set; }
    }
}
