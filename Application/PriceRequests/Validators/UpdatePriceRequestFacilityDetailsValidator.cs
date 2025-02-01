//using Application.PriceRequests.Dtos;
//using FluentValidation;

//namespace Application.PriceRequests.Validators
//{
//    public class UpdatePriceRequestFacilityDetailsValidator : AbstractValidator<UpdatePriceRequestFacilityDetailsDto>
//    {
//        public UpdatePriceRequestFacilityDetailsValidator()
//        {
//            RuleFor(d => d.FacilityName)
//              .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

//            RuleFor(d => d.FacilityEmail)
//                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

//            RuleFor(d => d.FacilityPhone)
//                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

//            RuleFor(d => d.FacilityAddress)
//                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

//            RuleFor(d => d.FacilitySize)
//                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

//            RuleFor(d => d.FacilityActivityType)
//                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

//            RuleFor(d => d.FacilityCommercialRegistrationNumber)
//                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

//            RuleFor(d => d.FacilityCommercialRegistrationExpiryDate)
//                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

//            RuleFor(d => d.FacilityLicenseNumber)
//                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

//            RuleFor(d => d.FacilityLicenseExpiryDate)
//                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

//            RuleFor(d => d.RepresentativeName)
//                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

//            RuleFor(d => d.RepresentativeEmail)
//                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

//            RuleFor(d => d.RepresentativePhone)
//                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

//            RuleFor(d => d.RepresentativeNationalId)
//                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

//            RuleFor(d => d.RepresentativeNationality)
//                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

//            RuleFor(d => d.CommissionerName)
//                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

//            RuleFor(d => d.CommissionerEmail)
//                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

//            RuleFor(d => d.CommissionerPhone)
//                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

//            RuleFor(d => d.CommissionerNationalId)
//                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);

//            RuleFor(d => d.CommissionerNationality)
//                .NotEmpty().WithState(_ => (int)ValidationMsgs.RequiredField);
//        }
//    }
//}
