using Application.ScheduleEntries.DTOs;
using Application.ScheduleEntries.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Contracts
{
    public class ScheduleEntriesController(IScheduleEntryService entryService) : ApiControllerBase
    {
        [HttpPost("Create")]
        public async Task<IActionResult> CreateScheduleEntriesAsync(long contractId, [FromBody] List<CreateScheduleEntryDto> scheduleEntries)
            => Result(await entryService.CreateScheduleEntriesAsync(contractId, scheduleEntries));

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateScheduleEntriesAsync(long contractId, [FromBody] List<UpdateScheduleEntryDto> scheduleEntries)
            => Result(await entryService.UpdateScheduleEntriesAsync(contractId, scheduleEntries));

        [HttpGet("GetById")]
        public async Task<IActionResult> GetScheduleEntryByIdAsync(long entryId)
       => Result(await entryService.GetScheduleEntryByIdAsync(entryId));

        [HttpGet("GetByContractId")]
        public async Task<IActionResult> GetScheduleEntriesByContractIdAsync(long contractId)
            => Result(await entryService.GetScheduleEntriesByContractIdAsync(contractId));


        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteScheduleEntryAsync(long entryId)
            => Result(await entryService.DeleteScheduleEntryAsync(entryId));
    }

}
