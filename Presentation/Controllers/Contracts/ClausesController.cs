using Application.Clauses.DTOs;
using Application.Clauses.Service;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Contracts
{
    public class ClausesController(IClauseService clauseService) : ApiControllerBase
    {
        [HttpPost("CreateCustomClauses")]
        public async Task<IActionResult> CreateCustomClausesAsync(long contractId, [FromBody] List<CreateCustomClauseDto> customClauses)
         => Result(await clauseService.CreateCustomClausesAsync(contractId, customClauses));



        [HttpPut("UpdateCustomClauses")]
        public async Task<IActionResult> UpdateCustomClausesAsync(long contractId, [FromBody] List<UpdateCustomClauseDto> customClauses)
        => Result(await clauseService.UpdateCustomClausesAsync(contractId, customClauses));


        [HttpDelete("DeleteCustomClause")]
        public async Task<IActionResult> DeleteCustomClauseAsync(long clauseId)
         => Result(await clauseService.DeleteCustomClauseAsync(clauseId));


    }
}
