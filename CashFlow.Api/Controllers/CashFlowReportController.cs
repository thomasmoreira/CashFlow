using CashFlow.Application.Dtos;
using CashFlow.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [ApiController]
    [Route("api/v1/cash-flow/report")]
    public class CashFlowReportController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IList<CashFlowResponseDto>),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReport()
        {
            return Ok();
        }
    }
}
