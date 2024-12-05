using Application.Tenants.Dtos;
using AutoMapper;

namespace Application.Tenants.Service
{
    internal class TenantService: ITenantService
    {
        private readonly IUnitOfWorkService _ufw;
        private readonly IMapper _mapper;

        public TenantService(IUnitOfWorkService ufw, IMapper mapper)
        {
            _ufw = ufw;
            _mapper = mapper;
        }

        public async Task<Result<int>> CreateAsync(TenantBriefDto dto)
        {
            var tenant = new Tenant 
            { 
                Name = dto.Name,
                Address = "xxxx",
                ContactInfo = "xxxx",
                SubscriptionPlan = "xxxx",
                CustomAttributes = "xxxx"
            };

            _ufw.Tenants.Create(tenant);
            await _ufw.SaveChangesAsync();

            return tenant.Id;
        }

        public async Task<Result<TenantBriefDto>> GetByIdAsync(int id)
        {
            var tenant = await _ufw.Tenants.GetByIdAsync(id);

            if (tenant == null)
                return new NotFoundException();

            return _mapper.Map<TenantBriefDto>(tenant);
        }
    }
}
