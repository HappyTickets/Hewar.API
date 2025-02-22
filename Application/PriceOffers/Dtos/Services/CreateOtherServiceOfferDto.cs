﻿namespace Application.PriceOffers.Dtos.Services
{
    public class CreateOtherServiceCostDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ShiftType ShiftType { get; set; }
        public decimal DailyCostPerUnit { get; set; }
        public decimal MonthlyCostPerUnit { get; set; }

    }
}
