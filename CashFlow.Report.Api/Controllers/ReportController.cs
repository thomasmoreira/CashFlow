using CashFlow.Application.Dtos;
using CashFlow.Application.Services.Interfaces;
using CashFlow.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
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