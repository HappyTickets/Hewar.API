﻿namespace Application.PriceRequests.Dtos.Offers
{
    public class PriceOfferServiceDto
    {
        public long ServiceId { get; set; }
        public int Quantity { get; set; }
        public decimal DailyCostPerUnit { get; set; }
        public decimal MonthlyCostPerUnit { get; set; }
        public ShiftType ShiftType { get; set; }
    }
}
