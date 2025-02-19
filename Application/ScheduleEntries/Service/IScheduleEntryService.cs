using Application.ScheduleEntries.DTOs;

namespace Application.ScheduleEntries.Service
{
    public interface IScheduleEntryService
    {
        Task<Result<Empty>> CreateScheduleEntriesAsync(long contractId, List<CreateScheduleEntryDto> scheduleEntries);
        Task<Result<Empty>> UpdateScheduleEntriesAsync(long contractId, List<UpdateScheduleEntryDto> scheduleEntries);
        Task<Result<Empty>> DeleteScheduleEntryAsync(long entryId);
        Task<Result<ScheduleEntryDto>> GetScheduleEntryByIdAsync(long entryId);
        Task<Result<List<ScheduleEntryDto>>> GetScheduleEntriesByContractIdAsync(long contractId);
    }

}
