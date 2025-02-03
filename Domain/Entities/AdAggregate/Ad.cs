﻿using Domain.Entities.AdAggregate;
using Domain.Entities.FacilityAggregate;

namespace Domain.Entities.InsuranceAdAggregate
{
    public class Ad : SoftDeletableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTimeOffset DatePosted { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public AdStatus Status { get; set; }
        public ContractType ContractType { get; set; }
        public long FacilityId { get; set; }
        public virtual Facility Facility { get; set; }

        public virtual ICollection<AdService> Services { get; set; } = new List<AdService>();
        public virtual ICollection<AdOffer>? AdOffers { get; set; }
    }
}
