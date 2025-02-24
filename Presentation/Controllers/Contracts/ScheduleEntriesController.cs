using Application.ScheduleEntries.DTOs;
using Application.ScheduleEntries.Service;
using Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Contracts
{
    [Authorize]
    public class ScheduleEntriesController(IScheduleEntryService entryService) : ApiControllerBase
    {
        [HttpPost("Create")]
        [HasPermission(Permissions.CreateScheduleEntry)]
        public async Task<IActionResult> CreateScheduleEntriesAsync(long contractId, [FromBody] List<CreateScheduleEntryDto> scheduleEntries)
            => Result(await entryService.CreateScheduleEntriesAsync(contractId, scheduleEntries));

        [HttpPut("Update")]
        [HasPermission(Permissions.UpdateScheduleEntry)]
        public async Task<IActionResult> UpdateScheduleEntriesAsync(long contractId, [FromBody] List<UpdateScheduleEntryDto> scheduleEntries)
            => Result(await entryService.UpdateScheduleEntriesAsync(contractId, scheduleEntries));

        [HttpGet("GetById")]
        [HasPermission(Permissions.ViewScheduleEntries)]
        public async Task<IActionResult> GetScheduleEntryByIdAsync(long entryId)
       => Result(await entryService.GetScheduleEntryByIdAsync(entryId));

        [HttpGet("GetByContractId")]
        [HasPermission(Permissions.ViewScheduleEntries)]
        public async Task<IActionResult> GetScheduleEntriesByContractIdAsync(long contractId)
            => Result(await entryService.GetScheduleEntriesByContractIdAsync(contractId));


        [HttpDelete("Delete")]
        [HasPermission(Permissions.DeleteScheduleEntry)]
        public async Task<IActionResult> DeleteScheduleEntryAsync(long entryId)
            => Result(await entryService.DeleteScheduleEntryAsync(entryId));
    }

}
