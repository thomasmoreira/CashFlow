namespace CashFlow.Application.Dtos
{
    public class CashFlowResponseDto
    {
        public DateOnly Date { get; set; }
        public decimal Expenses { get; set; }
        public decimal Incomes { get; set; }
        public decimal Balance { get; set; }
        public decimal CumulativeBalance { get; set; }
    }
}
