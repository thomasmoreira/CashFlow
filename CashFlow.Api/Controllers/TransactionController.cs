using CashFlow.Application.Dtos;
using CashFlow.Application.Services.Interfaces;
using CashFlow.Application.Validations;
using CashFlow.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CashFlow.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

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
                return BadRequest(resultValidator.Errors.Select(e => new { e.ErrorMessage }).ToList());

            var result = await _transactionService.AddTransaction(transaction);

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "employee, manager")]
        [ProducesResponseType(typeof(IList<TransactionResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactions() 
        {
            var result = await _transactionService.GetTransactions();
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "employee, manager")]
        [ProducesResponseType(typeof(TransactionResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactionById([FromRoute] Guid id)
        {
            return Ok();
        }      
    }
}
