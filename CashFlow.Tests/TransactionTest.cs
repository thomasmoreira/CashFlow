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
        private AddTransactionDto transactionWithInvalidTransactionDate;
        private AddTransactionDto validTransaction;
        private AddTransactionDto invalidTransaction;

        [SetUp]
        public void Setup()
        {
            validator = new AddTransactionValidator();

            validTransaction = new AddTransactionDto
            {
                AccountType = (int)EAccountType.CurrentAccount,
                TransactionDate = DateTime.Now,
                TransactionType = (int)ETransactionType.Debit,
                Description = "Teste transacion valida",
                Amount = 200
            };

            invalidTransaction = new AddTransactionDto
            {
                AccountType = 5,
                TransactionDate = DateTime.MinValue,
                TransactionType = 4,
                Description = "Teste transacion type invalido",
                Amount = 0
            };

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


            transactionWithInvalidTransactionDate = new AddTransactionDto
            {
                AccountType = (int)EAccountType.CurrentAccount,
                TransactionDate = DateTime.MinValue,
                TransactionType = 4,
                Description = "Teste transacion type invalido",
                Amount = 0
            };


        }

        [Test]
        public void Should_have_any_error_when_transaction_is_invalid()
        {
            var result = validator.TestValidate(invalidTransaction);
            result.ShouldHaveAnyValidationError();
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
        public void Should_have_error_when_transaction_type_is_invalid()
        {
            var result = validator.TestValidate(transactionWithInvalidTransactionType);
            result.ShouldHaveValidationErrorFor(person => person.TransactionType);
        }

        [Test]
        public void Should_have_error_when_transaction_date_is_invalid()
        {
            var result = validator.TestValidate(transactionWithInvalidTransactionDate);
            result.ShouldHaveValidationErrorFor(person => person.TransactionDate);
        }

        [Test]
        public void Should_not_have_error_when_transaction_is_valid()
        {
            var result = validator.TestValidate(validTransaction);
            result.ShouldNotHaveAnyValidationErrors();
        }


    }
}