using Application.Guards.Dtos;
using AutoMapper;
using Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Guards.Service
{
    internal class GuardsService: IGuardsService
    {
        private readonly IUnitOfWorkService _ufw;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public GuardsService(IUnitOfWorkService ufw, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _ufw = ufw;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Result<Empty>> UpdateAsync(UpdateGuardDto dto)
        {
            var guard = await _ufw.Guards.GetByIdAsync(dto.Id, ["LoginDetails"]);

            if (guard == null)
                return new NotFoundException();

            if (await _userManager.Users.AnyAsync(u => u.Email == dto.Email && u.Guard.Id != dto.Id))
                return new ConflictException(Resource.EmailExistsError);

            if (await _userManager.Users.AnyAsync(u => u.UserName == dto.UserName && u.Guard.Id != dto.Id))
                return new ConflictException(Resource.UserNameExistsError);

            guard.FirstName = dto.FirstName;
            guard.LastName = dto.LastName;
            guard.DateOfBirth = dto.DateOfBirth;
            guard.Skills = dto.Skills;
            guard.LoginDetails.UserName = dto.UserName;
            guard.LoginDetails.Email = dto.Email;
            guard.LoginDetails.PhoneNumber = dto.Phone;

            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<GuardDto>> GetByIdAsync(long id)
        {
            var guard = await _ufw.Guards.GetByIdAsync(id, ["LoginDetails"]);

            if (guard == null)
                return new NotFoundException();

            return _mapper.Map<GuardDto>(guard);
        }

        public async Task<Result<GuardDto[]>> GetAllAsync()
        {
            var guards = await _ufw.Guards.GetAllAsync(["LoginDetails"]);

            return _mapper.Map<GuardDto[]>(guards);
        }

        public async Task<Result<Empty>> SoftDeleteAsync(long id)
        {
            var guard = await _ufw.Guards.GetByIdAsync(id, ["LoginDetails"]);

            if (guard == null)
                return new NotFoundException();

            _ufw.Guards.SoftDelete(guard);
            await _ufw.SaveChangesAsync();

            return Empty.Default;
        }

        public async Task<Result<Empty>> HardDeleteAsync(long id)
        {
            var guard = await _ufw.Guards.GetByIdAsync(id, ["LoginDetails"]);

            if (guard == null)
                return new NotFoundException();

            using (var tran = await _ufw.BeginTransactionAsync())
            {
                try
                {
                    _ufw.Guards.HardDelete(guard);
                    await _userManager.DeleteAsync(guard.LoginDetails);
                    await _ufw.SaveChangesAsync();

                    await tran.CommitAsync();
                }
                catch
                {
                    await tran.RollbackAsync();
                }
            }

            return Empty.Default;
        }
    }
}
