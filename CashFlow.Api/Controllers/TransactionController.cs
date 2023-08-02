using CashFlow.Application.Dtos;
using CashFlow.Application.Extensions;
using CashFlow.Application.Services.Interfaces;
using CashFlow.Application.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Serilog;
using ILogger = Serilog.ILogger;

namespace CashFlow.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private ILogger _logger = Log.ForContext<TransactionController>();

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        [ProducesResponseType(typeof(AddTransactionResponseDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddTransaction([FromBody] AddTransactionDto transaction)
        {
            var transactionValidator = new AddTransactionValidator();
            var resultValidator = transactionValidator.Validate(transaction);

            if (!resultValidator.IsValid) 
            {
                var errors = resultValidator.Errors.Select(e => e.ErrorMessage).ToList();

                _logger.ForContext("Errors", errors.ToJson())
                    .Error("Modelo inválido");

                return new BadRequestObjectResult(new
                {
                    notifications = errors
                });
            }                

            var result = await _transactionService.AddTransaction(transaction);

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "employee, manager")]
        [ProducesResponseType(typeof(IList<TransactionResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactions([FromQuery] GetTransactionsDto getTransactionsDto) 
        {
            var getTrasanctionsValidator = new GetTransactionsValidator();
            var resultValidator = getTrasanctionsValidator.Validate(getTransactionsDto);

            if (!resultValidator.IsValid)
            {
                var errors = resultValidator.Errors.Select(e => e.ErrorMessage).ToList();

                _logger.ForContext("Errors", errors.ToJson())
                    .Error("Modelo inválido");

                return new BadRequestObjectResult(new
                {
                    notifications = errors
                });
            }


            var result = await _transactionService.GetTransactions(getTransactionsDto);
            return Ok(result);
        }

    }
}
