using CashFlow.Application.Dtos;
using CashFlow.Domain.Enums;
using FluentValidation;

namespace CashFlow.Application.Validations
{
    public class AddAccountValidator : Validator<AddAccountDto>
    {
        public AddAccountValidator()
        {
            RuleFor(a => a.Name)
                .Must(name => NotIsNullOrEmpyt(name)).WithMessage(ValidationMessages.CampoVazio(nameof(AddAccountDto.Name)))
                .MaximumLength(50).WithMessage(ValidationMessages.CampoTamanhoMaximoInvalido(nameof(AddAccountDto.Name),50));
                
            RuleFor(a => a.AccountType)
                .Must(at => IsValidEnum<EAccountType>(at)).WithMessage(ValidationMessages.CampoInvalido(nameof(AddAccountDto.AccountType)));

        }
    }
}
