using CashFlow.Application.Dtos;
using CashFlow.Application.Services.Interfaces;
using CashFlow.Application.Validations;
using CashFlow.Domain.Enums;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CashFlow.Api.Controllers
{
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
        //[Authorize(PrivilegeConst.ReadUser)]
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
        //[Authorize]
        [ProducesResponseType(typeof(IList<TransactionResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactions() 
        {
            var result = await _transactionService.GetTransactions();
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        //[Authorize]
        [ProducesResponseType(typeof(TransactionResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactionById([FromRoute] Guid id)
        {
            return Ok();
        }

        [HttpGet("get-balance")]
        public async Task<IActionResult> GetBalance([FromQuery] DateTime dateBalance) 
        {
            var creditTransactions = (await _transactionService.GetTransactions(dateBalance)).Where(t => t.TransactionType == (int)ETransactionType.Credit).ToList();
            var debitTransactions = (await _transactionService.GetTransactions(dateBalance)).Where(t => t.TransactionType == (int)ETransactionType.Debit).ToList();
            var expenses = debitTransactions.Sum(dt => dt.AmountCents);
            var incomes = creditTransactions.Sum(ct => ct.AmountCents);

            var report = new CashFlowReportDto 
            { 
                Date = dateBalance.Date.ToString("dd/MM/yyyy"), 
                Incomes = incomes.ToString("C2", CultureInfo.CurrentCulture), 
                Expenses = expenses.ToString("C2", CultureInfo.CurrentCulture), 
                Balance = (incomes - expenses).ToString("C2", CultureInfo.CurrentCulture)
            };

            return Ok(report);
        }
    }
}
