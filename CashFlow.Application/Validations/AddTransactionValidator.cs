using CashFlow.Application.Dtos;
using CashFlow.Domain.Enums;
using FluentValidation;

namespace CashFlow.Application.Validations
{
    public class AddTransactionValidator : Validator<AddTransactionDto>
    {
        public AddTransactionValidator()
        {
            RuleFor(t => t.TransactionDate)
                .NotNull().WithMessage(ValidationMessages.CampoVazio(nameof(AddTransactionDto.TransactionDate)))
                .Must(td => IsValidDate(td)).WithMessage(ValidationMessages.CampoInvalido(nameof(AddTransactionDto.TransactionDate)));

            RuleFor(t => t.Amount)
                .NotNull().WithMessage(ValidationMessages.CampoVazio(nameof(AddTransactionDto.Amount)))
                .Must(ac => IsValidNumber(ac)).WithMessage(ValidationMessages.CampoInvalido(nameof(AddTransactionDto.Amount)));

            RuleFor(t => t.Description)
                .MaximumLength(100).WithMessage(ValidationMessages.CampoTamanhoMaximoInvalido(nameof(AddTransactionDto.Description),100));

            RuleFor(t => t.TransactionType)
                .Must(tt => IsValidEnum<ETransactionType>(tt)).WithMessage(ValidationMessages.CampoInvalido(nameof(AddTransactionDto.TransactionType)));
            
                

        }
    }
}
