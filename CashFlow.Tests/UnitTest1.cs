using CashFlow.Application.Dtos;
using CashFlow.Application.Validations;
using CashFlow.Domain.Enums;
using FluentValidation.TestHelper;

namespace CashFlow.Tests
{
    [TestFixture]
    public class Tests
    {
        private AddTransactionValidator validator;
        private AddTransactionDto transactionWithInvalidAmount;
        private AddTransactionDto transactionWithInvalidAccountType;
        private AddTransactionDto transactionWithInvalidTransactionType;

        [SetUp]
        public void Setup()
        {
            validator = new AddTransactionValidator();
            transactionWithInvalidAmount = new AddTransactionDto 
            { 
                AccountType = (int)EAccountType.CurrentAccount, 
                TransactionDate = DateTime.Now, 
                TransactionType = (int)ETransactionType.Credit, 
                Description = "Teste valor errado",
                Amount = 0
            };

            transactionWithInvalidAccountType = new AddTransactionDto
            {
                AccountType = 5,
                TransactionDate = DateTime.Now,
                TransactionType = (int)ETransactionType.Credit,
                Description = "Teste account type invalido",
                Amount = 200
            };

            transactionWithInvalidTransactionType = new AddTransactionDto
            {
                AccountType = (int)EAccountType.CurrentAccount,
                TransactionDate = DateTime.Now,
                TransactionType = 4,
                Description = "Teste transacion type invalido",
                Amount = 0
            };
        }

        [Test]
        public void Should_have_error_when_Amount_is_invalid()
        {            
            var result = validator.TestValidate(transactionWithInvalidAmount);
            result.ShouldHaveValidationErrorFor(person => person.Amount);
        }

        [Test]
        public void Should_have_error_when_account_type_is_invalid()
        {
            var result = validator.TestValidate(transactionWithInvalidAccountType);
            result.ShouldHaveValidationErrorFor(person => person.AccountType);
        }

        [Test]
        public void Should_have_error_when_transactio_type_is_invalid()
        {
            var result = validator.TestValidate(transactionWithInvalidTransactionType);
            result.ShouldHaveValidationErrorFor(person => person.TransactionType);
        }
    }
}