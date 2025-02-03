using Application.Hewar.Dtos;
using AutoMapper;
using Domain.Entities.Hewar;

namespace Application.Hewar.Service
{
    public class HewarProvidedService(IUnitOfWorkService unitOfWork, IMapper mapper) : IHewarProvidedService
    {
        public async Task<Result<long>> CreateAsync(CreateHewarServiceDto dto, CancellationToken cancellationToken = default)
        {
            var hewarServiceRepo = unitOfWork.GetRepository<HewarService>();

            var hewarService = mapper.Map<HewarService>(dto);

            await hewarServiceRepo.CreateAsync(hewarService);

            await unitOfWork.SaveChangesAsync();

            var hewarServiceDto = mapper.Map<HewarServiceDto>(hewarService);
            return Result<long>.Success(hewarService.Id, SuccessCodes.HewarServiceCreated);
        }

        public async Task<Result<HewarServiceDto>> GetByIdAsync(long id)
        {
            var hewarService = await unitOfWork.GetRepository<HewarService>().GetByIdAsync(id);

            if (hewarService is null)
                return new NotFoundError();

            var hewarServiceDto = mapper.Map<HewarServiceDto>(hewarService);
            return Result<HewarServiceDto>.Success(hewarServiceDto, SuccessCodes.HewarServiceReceived);
        }

        public async Task<Result<List<HewarServiceDto>>> GetAllAsync()
        {
            var hewarServices = await unitOfWork.GetRepository<HewarService>().GetAllAsync();

            var hewarServiceDtos = mapper.Map<List<HewarServiceDto>>(hewarServices);
            return Result<List<HewarServiceDto>>.Success(hewarServiceDtos, SuccessCodes.HewarServiceReceived);
        }

        public async Task<Result<Empty>> UpdateAsync(HewarServiceDto dto)
        {
            var hewarService = await unitOfWork.GetRepository<HewarService>().GetByIdAsync(dto.Id);

            if (hewarService is null)
                return new NotFoundError();

            hewarService.Name = dto.Name;
            hewarService.Description = dto.Description;

            await unitOfWork.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.HewarServiceCreated);
        }

        public async Task<Result<Empty>> DeleteAsync(long id)
        {
            var hewarService = await unitOfWork.GetRepository<HewarService>().GetByIdAsync(id);

            if (hewarService is null) return new NotFoundError();

            unitOfWork.GetRepository<HewarService>().HardDelete(hewarService);
            await unitOfWork.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.Created);
        }
    }
}
