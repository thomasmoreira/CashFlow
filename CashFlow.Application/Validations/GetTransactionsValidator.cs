using CashFlow.Application.Dtos;
using FluentValidation;

namespace CashFlow.Application.Validations
{
    public class GetTransactionsValidator : Validator<GetTransactionsDto>
    {
        public GetTransactionsValidator()
        {
            RuleFor(t => t.TransactionDate)
                .Must(d => IsValidDate(d)).WithMessage(ValidationMessages.CampoInvalido(nameof(GetTransactionsDto.TransactionDate)));
        }
    }
}
