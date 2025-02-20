using Application.ScheduleEntries.DTOs;
using Domain.Entities.ContractAggregate.Dynamic;

namespace Application.ScheduleEntries.Service
{
    public class ScheduleEntryService(IUnitOfWorkService ufw) : IScheduleEntryService
    {
        public async Task<Result<Empty>> CreateScheduleEntriesAsync(long contractId, List<CreateScheduleEntryDto> scheduleEntries)
        {
            var contract = await ufw.GetRepository<Contract>().FirstOrDefaultAsync(c => c.Id == contractId);
            if (contract is null)
                return new NotFoundError();

            var newEntries = scheduleEntries.Select(se => new ScheduleEntry
            {
                ContractId = contractId,
                LocationAr = se.LocationAr,
                LocationEn = se.LocationEn,
                GuardsRequired = se.GuardsRequired,
                ShiftTimeAr = se.ShiftTimeAr,
                ShiftTimeEn = se.ShiftTimeEn,
                NotesAr = se.NotesAr,
                NotesEn = se.NotesEn
            }).ToList();

            ufw.GetRepository<ScheduleEntry>().CreateRange(newEntries);
            await ufw.SaveChangesAsync();

            return Result<Empty>.Success(Empty.Default, SuccessCodes.ScheduleEntryCreated);
        }

        public async Task<Result<Empty>> UpdateScheduleEntriesAsync(long contractId, List<UpdateScheduleEntryDto> scheduleEntries)
        {
            if (!scheduleEntries.Any())
                return Result<Empty>.Success(Empty.Default);

            var contract = await ufw.GetRepository<Contract>().FirstOrDefaultAsync(c => c.Id == contractId, [nameof(Contract.ScheduleEntries)]);
            if (contract is null || contract.ScheduleEntries is null)
                return new NotFoundError();

            var updateEntryIds = scheduleEntries.Select(se => se.Id).ToList();
            var existingEntryIds = contract.ScheduleEntries.Select(se => se.Id).ToList();
            var invalidEntryIds = updateEntryIds.Except(existingEntryIds).ToList();
            if (invalidEntryIds.Any())
                return new ConflictError();

            var updateLookup = scheduleEntries.ToDictionary(se => se.Id);
            foreach (var entry in contract.ScheduleEntries)
            {
                if (updateLookup.TryGetValue(entry.Id, out var updatedEntry))
                {
                    entry.LocationAr = updatedEntry.LocationAr;
                    entry.LocationEn = updatedEntry.LocationEn;
                    entry.GuardsRequired = updatedEntry.GuardsRequired;
                    entry.ShiftTimeAr = updatedEntry.ShiftTimeAr;
                    entry.ShiftTimeEn = updatedEntry.ShiftTimeEn;
                    entry.NotesAr = updatedEntry.NotesAr;
                    entry.NotesEn = updatedEntry.NotesEn;
                }
            }

            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.ScheduleEntryUpdated);
        }

        public async Task<Result<Empty>> DeleteScheduleEntryAsync(long scheduleEntryId)
        {
            var scheduleEntry = await ufw.GetRepository<ScheduleEntry>().GetByIdAsync(scheduleEntryId);
            if (scheduleEntry is null)
                return new NotFoundError();

            ufw.GetRepository<ScheduleEntry>().HardDelete(scheduleEntry);
            await ufw.SaveChangesAsync();
            return Result<Empty>.Success(Empty.Default, SuccessCodes.ScheduleEntryDeleted);
        }

        public async Task<Result<ScheduleEntryDto>> GetScheduleEntryByIdAsync(long entryId)
        {
            var entry = await ufw.GetRepository<ScheduleEntry>().GetByIdAsync(entryId);
            if (entry is null)
                return new NotFoundError();

            var dto = new ScheduleEntryDto
            {
                Id = entry.Id,
                ContractId = entry.ContractId,
                LocationAr = entry.LocationAr,
                LocationEn = entry.LocationEn,
                GuardsRequired = entry.GuardsRequired,
                ShiftTimeAr = entry.ShiftTimeAr,
                ShiftTimeEn = entry.ShiftTimeEn,
                NotesAr = entry.NotesAr,
                NotesEn = entry.NotesEn
            };

            return Result<ScheduleEntryDto>.Success(dto);
        }

        public async Task<Result<List<ScheduleEntryDto>>> GetScheduleEntriesByContractIdAsync(long contractId)
        {
            var entries = await ufw.GetRepository<ScheduleEntry>().FilterAsync(e => e.ContractId == contractId);

            var dtos = entries.Select(e => new ScheduleEntryDto
            {
                Id = e.Id,
                ContractId = e.ContractId,
                LocationAr = e.LocationAr,
                LocationEn = e.LocationEn,
                GuardsRequired = e.GuardsRequired,
                ShiftTimeAr = e.ShiftTimeAr,
                ShiftTimeEn = e.ShiftTimeEn,
                NotesAr = e.NotesAr,
                NotesEn = e.NotesEn
            }).ToList();

            return Result<List<ScheduleEntryDto>>.Success(dtos);
        }

    }

}
