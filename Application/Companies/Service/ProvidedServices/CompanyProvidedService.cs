using Application.Companies.Dtos.ProvidedServices;
using Application.Companies.Service.ServicesProvided;
using AutoMapper;
using Domain.Entities.CompanyAggregate;

namespace Application.Companies.Service.ProvidedServices
{
    public class CompanyProvidedService(IUnitOfWorkService unitOfWork, IMapper mapper, ICurrentUserService currentUser) : ICompanyProvidedService
    {
        public async Task<Result<long>> CreateAsync(CreateCompanyServiceDto dto, CancellationToken cancellationToken = default)
        {

            if (currentUser.EntityId is null)
                return new UnauthorizedError();

            var currentCompany = await unitOfWork
                .GetRepository<Company>()
                .GetByIdAsync(currentUser.EntityId.Value);

            var companyService = mapper.Map<CompanyService>(dto);
            if (currentCompany is null)
                return new NotFoundError();

            currentCompany.Services.Add(companyService);

            await unitOfWork.SaveChangesAsync();

            var companyServiceDto = mapper.Map<CompanyServiceDto>(companyService);
            return Result<long>.Success(companyService.Id, SuccessCodes.CompanyServiceCreated);
        }

        public async Task<Result<CompanyServiceDto>> GetByIdAsync(long id)
        {
            var companyService = await unitOfWork.GetRepository<CompanyService>().GetByIdAsync(id);

            if (companyService is null)
                return new NotFoundError();

            var companyServiceDto = mapper.Map<CompanyServiceDto>(companyService);
            return Result<CompanyServiceDto>.Success(companyServiceDto, SuccessCodes.CompanyServiceReceived);
        }

        public async Task<Result<List<CompanyServiceDto>>> GetAllAsync()
        {
            var companyServices = await unitOfWork.GetRepository<CompanyService>().GetAllAsync();

            var companyServiceDtos = mapper.Map<List<CompanyServiceDto>>(companyServices);
            return Result<List<CompanyServiceDto>>.Success(companyServiceDtos, SuccessCodes.CompanyServiceReceived);
        }

        // Update a CompanyService
        public async Task<Result<Empty>> UpdateAsync(UpdateCompanyServiceDto dto)
        {
            var companyService = await unitOfWork.GetRepository<CompanyService>().GetByIdAsync(dto.Id);

            if (companyService == null)
                return new NotFoundError();

            companyService.Name = dto.Name;
            companyService.Description = dto.Description;

            await unitOfWork.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.CompanyServiceCreated);
        }

        public async Task<Result<Empty>> DeleteAsync(long id)
        {
            var companyService = await unitOfWork.GetRepository<CompanyService>().GetByIdAsync(id);

            if (companyService == null)
                return new NotFoundError();

            unitOfWork.GetRepository<CompanyService>().HardDelete(companyService);
            await unitOfWork.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.Created);
        }

        public async Task<Result<List<CompanyServiceDto>>> GetServicesByCompanyIdAsync(long companyId)
        {
            var companyServices = await unitOfWork
                .GetRepository<CompanyService>()
                .FilterAsync(cs => cs.CompanyId == companyId);

            var companyServiceDtos = mapper.Map<List<CompanyServiceDto>>(companyServices);
            return Result<List<CompanyServiceDto>>.Success(companyServiceDtos, SuccessCodes.CompanyServiceReceived);
        }
    }

}
