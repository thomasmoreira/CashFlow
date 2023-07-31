using FluentValidation;

namespace CashFlow.Application.Validations
{
    public class Validator<TModel> : AbstractValidator<TModel>
    {
        protected bool IsValidGuid(string value) => Guid.TryParse(value, out _);
        protected bool IsValidEnum<T>(int value) => Enum.IsDefined(typeof(T), value);
        protected bool NotIsNullOrEmpyt(string value) => !string.IsNullOrEmpty(value);
        protected bool IsValidDate(DateTime date) => !date.Equals(default);
        protected bool IsValidNumber(object value)
        {
            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}
