using Application.Clauses.DTOs;
using Application.Clauses.Service;
using Infrastructure.Authentication.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Contracts
{
    [Authorize]
    public class ClausesController(IClauseService clauseService) : ApiControllerBase
    {
        [HttpPost("CreateCustomClauses")]
        [HasPermission(Permissions.CreateCustomClause)]
        public async Task<IActionResult> CreateCustomClausesAsync(long contractId, [FromBody] List<CreateCustomClauseDto> customClauses)
         => Result(await clauseService.CreateCustomClausesAsync(contractId, customClauses));



        [HttpPut("UpdateCustomClauses")]
        [HasPermission(Permissions.UpdateCustomClause)]
        public async Task<IActionResult> UpdateCustomClausesAsync(long contractId, [FromBody] List<UpdateCustomClauseDto> customClauses)
        => Result(await clauseService.UpdateCustomClausesAsync(contractId, customClauses));

        [HttpGet("GetCustomClauseById")]
        [HasPermission(Permissions.ViewCustomClauses)]
        public async Task<IActionResult> GetCustomClauseByIdAsync(long clauseId)
       => Result(await clauseService.GetCustomClauseByIdAsync(clauseId));

        [HttpGet("GetCustomClausesByContractId")]
        [HasPermission(Permissions.ViewCustomClauses)]
        public async Task<IActionResult> GetCustomClausesByContractIdAsync(long contractId)
            => Result(await clauseService.GetCustomClausesByContractIdAsync(contractId));


        [HttpDelete("DeleteCustomClause")]
        [HasPermission(Permissions.DeleteCustomClause)]
        public async Task<IActionResult> DeleteCustomClauseAsync(long clauseId)
         => Result(await clauseService.DeleteCustomClauseAsync(clauseId));


    }
}
