using CashFlow.Application.Dtos;
using CashFlow.Application.Services.Interfaces;
using CashFlow.Application.Validations;
using CashFlow.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace CashFlow.Report.Api.Controllers
{
    [ApiController]
    [Route("api/v1/cash-flow/report")]
    public class ReportController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public ReportController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBalance([FromQuery] GetTransactionsDto getTransactionsDto) 
        {
            var getTrasanctionsValidator = new GetTransactionsValidator();
            var resultValidator = getTrasanctionsValidator.Validate(getTransactionsDto);

            if (!resultValidator.IsValid)
            {
                var errors = resultValidator.Errors.Select(e => e.ErrorMessage).ToList();

                return new BadRequestObjectResult(new
                {
                    notifications = errors
                });
            }

            var creditTransactions = (await _transactionService.GetTransactions(getTransactionsDto)).Where(t => t.TransactionType == (int)ETransactionType.Credit).ToList();
            var debitTransactions = (await _transactionService.GetTransactions(getTransactionsDto)).Where(t => t.TransactionType == (int)ETransactionType.Debit).ToList();
            var expenses = debitTransactions.Sum(dt => (double)dt.AmountCents/100);
            var incomes =  creditTransactions.Sum(ct => (double)ct.AmountCents/100);

            var report = new CashFlowReportDto
            {
                Date = getTransactionsDto.TransactionDate.Date.ToString("dd/MM/yyyy"),
                Incomes = incomes.ToString("C2", CultureInfo.CurrentCulture),
                Expenses = expenses.ToString("C2", CultureInfo.CurrentCulture),
                Balance = (incomes - expenses).ToString("C2", CultureInfo.CurrentCulture)
            };

            return Ok(report);

        }
    }
}