//using Application.Account.Service.Interfaces;
//using Application.Guards.Dtos;
//using AutoMapper;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//namespace Application.Guards.Service
//{
//    internal class GuardsService(IUnitOfWorkService ufw, IMapper mapper, UserManager<ApplicationUser> userManager, IRegistrationService registrationService) : IGuardsService
//    {
//        public async Task<Result<Empty>> CreateAsync(CreateGuardDto dto)
//        {
//            var validationResult = await registrationService.ValidateRegistrationAsync(dto.Phone, dto.Email, Roles.Guard);
//            if (validationResult != null) return validationResult;

//            var user = registrationService.CreateUserBase(dto.Email, dto.Phone, AccountTypes.Guard, dto.ImageUrl, true);
//            user.UserName = dto.UserName;
//            user.Guard = new Guard
//            {
//                FirstName = dto.FirstName,
//                LastName = dto.LastName,
//                DateOfBirth = dto.DateOfBirth,
//                NationalId = dto.NationalId,
//                Qualification = dto.Qualification,
//                City = dto.City,
//                BloodType = dto.BloodType,
//                Height = dto.Height,
//                Weight = dto.Weight,
//                Skills = mapper.Map<ICollection<Skill>>(dto.Skills),
//                PrevCompanies = mapper.Map<ICollection<PrevCompany>>(dto.PrevCompanies),
//            };

//            var res = await registrationService.RegisterAccountAsync(user, dto.Password, Roles.Guard);

//            return res.IsSuccess ? Result<Empty>.Success(Empty.Default, SuccessCodes.GuardCreated) : res;
//        }

//        public async Task<Result<Empty>> UpdateAsync(UpdateGuardDto dto)
//        {
//            var guard = await ufw.Guards.GetByIdAsync(dto.Id, [nameof(Guard.LoginDetails)]);

//            if (guard == null)
//                return new NotFoundError();

//            if (await userManager.Users.AnyAsync(u => u.PhoneNumber == dto.Phone && u.Guard.Id != dto.Id))
//                return new ConflictError(ErrorCodes.PhoneExists);

//            if (await userManager.Users.AnyAsync(u => u.Email == dto.Email && u.Guard.Id != dto.Id))
//                return new ConflictError(ErrorCodes.EmailExists);

//            if (await userManager.Users.AnyAsync(u => u.UserName == dto.UserName && u.Guard.Id != dto.Id))
//                return new ConflictError(ErrorCodes.UserNameExists);

//            guard.FirstName = dto.FirstName;
//            guard.LastName = dto.LastName;
//            guard.DateOfBirth = dto.DateOfBirth;
//            guard.NationalId = dto.NationalId;
//            guard.Qualification = dto.Qualification;
//            guard.City = dto.City;
//            guard.BloodType = dto.BloodType;
//            guard.Height = dto.Height;
//            guard.Weight = dto.Weight;
//            guard.Skills = mapper.Map<ICollection<Skill>>(dto.Skills);
//            guard.PrevCompanies = mapper.Map<ICollection<PrevCompany>>(dto.PrevCompanies);
//            guard.LoginDetails.UserName = dto.UserName;
//            guard.LoginDetails.Email = dto.Email;
//            guard.LoginDetails.PhoneNumber = dto.Phone;
//            guard.LoginDetails.ImageUrl = dto.ImageUrl;


//            await ufw.SaveChangesAsync();

//            return Result<Empty>.Success(Empty.Default, SuccessCodes.GuardUpdated);

//        }

//        public async Task<Result<GuardDto>> GetByIdAsync(long id)
//        {
//            var guard = await ufw.Guards.GetByIdAsync(id, [nameof(Guard.LoginDetails)]);

//            if (guard == null)
//                return new NotFoundError();

//            var guardDto = mapper.Map<GuardDto>(guard);
//            return Result<GuardDto>.Success(guardDto, SuccessCodes.GuardReceived);

//        }

//        public async Task<Result<GuardDto[]>> GetAllAsync()
//        {
//            var guards = await ufw.Guards.GetAllAsync([nameof(Guard.LoginDetails)]);

//            var guardsDto = mapper.Map<GuardDto[]>(guards);
//            return Result<GuardDto[]>.Success(guardsDto, SuccessCodes.GuardsReceived);

//        }

//        public async Task<Result<Empty>> SoftDeleteAsync(long id)
//        {
//            var guard = await ufw.Guards.GetByIdAsync(id, [nameof(Guard.LoginDetails)]);

//            if (guard == null)
//                return new NotFoundError();

//            ufw.Guards.SoftDelete(guard);
//            await ufw.SaveChangesAsync();

//            return Result<Empty>.Success(Empty.Default, SuccessCodes.GuardSoftDeleted);

//        }

//        public async Task<Result<Empty>> HardDeleteAsync(long id)
//        {
//            var guard = await ufw.Guards.GetByIdAsync(id, [nameof(Guard.LoginDetails)]);

//            if (guard == null)
//                return new NotFoundError();

//            using (var tran = await ufw.BeginTransactionAsync())
//            {
//                try
//                {
//                    ufw.Guards.HardDelete(guard);
//                    await userManager.DeleteAsync(guard.LoginDetails);
//                    await ufw.SaveChangesAsync();

//                    await tran.CommitAsync();
//                }
//                catch
//                {
//                    await tran.RollbackAsync();
//                }
//            }

//            return Result<Empty>.Success(Empty.Default, SuccessCodes.GuardHardDeleted);

//        }
//    }
//}
