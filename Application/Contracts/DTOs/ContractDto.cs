using Application.Contracts.DTOs.Static;
using Application.PriceOffers.Dtos.Services;

namespace Application.Contracts.DTOs
{
    public class ContractDto
    {
        public long ContractId { get; set; }
        public BilingualText ContractTitle { get; set; }
        public Preamble Preamble { get; set; } // filled through contract fields
        public List<Clause> Clauses { get; set; }
        public List<BilingualText> CustomClauses { get; set; } // HTML from contract fields

        public List<ScheduleEntry> ScheduleEntries { get; set; } // totally from contract fields
        public List<GetServiceOfferDto> ServicesOffer { get; set; } = new List<GetServiceOfferDto>(); // from offer
        public List<GetOtherServiceOfferDto> OtherServicesOffer { get; set; } = new List<GetOtherServiceOfferDto>(); // from offer

        public List<BilingualText> Duties_Services { get; set; }

        public BilingualText ClosingRemark { get; set; }
        public Signatures Signatures { get; set; } = new Signatures();

    }
}
