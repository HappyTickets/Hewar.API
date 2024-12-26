using Application.InsuranceAds.Dtos;
using AutoMapper;

namespace Application.InsuranceAds.Service
{
    internal class InsuranceAdsService: IInsuranceAdsService
    {
        private readonly IUnitOfWorkService _ufw;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public InsuranceAdsService(IUnitOfWorkService ufw, IMapper mapper, ICurrentUserService currentUser)
        {
            _ufw = ufw;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<Empty>> CreateAdAsync(CreateInsuranceAdDto dto)
        {
            var ad = _mapper.Map<InsuranceAd>(dto);
            ad.FacilityId = _currentUser.Id!.Value;

            _ufw.InsuranceAds.Create(ad);
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<Empty>> UpdateAdAsync(UpdateInsuranceAdDto dto)
        {
            var ad = await _ufw.InsuranceAds.GetByIdAsync(dto.Id);

            if (ad == null)
                return new NotFoundException();

            _mapper.Map(dto, ad);
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<InsuranceAdDto>> GetAdByIdAsync(long id)
        {
            var ad = await _ufw.InsuranceAds.GetByIdAsync(id, ["Facility.LoginDetails"]);

            if (ad == null)
                return new NotFoundException();

            return _mapper.Map<InsuranceAdDto>(ad);
        }

        public async Task<Result<InsuranceAdDto[]>> GetMyAdsAsync()
        {
            var ads = await _ufw.InsuranceAds
                .FilterAsync(ad => ad.FacilityId == _currentUser.Id!, ["Facility.LoginDetails"]);

            return _mapper.Map<InsuranceAdDto[]>(ads);
        }
        
        public async Task<Result<InsuranceAdDto[]>> GetOpenedAdsAsync()
        {
            var ads = await _ufw.InsuranceAds
                .FilterAsync(ad => ad.Status == InsuranceAdStatus.Opened, ["Facility.LoginDetails"]);

            return _mapper.Map<InsuranceAdDto[]>(ads);
        }
    }
}
