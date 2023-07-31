using CashFlow.Application.Attributes;
using CashFlow.Application.Dtos;
using CashFlow.Application.Services.Interfaces;
using CashFlow.Application.Validations;
using CashFlow.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize(ERole.Admin)]
        [HttpPost]
        [ProducesResponseType(typeof(AddAccountResponseDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddAccount([FromBody] AddAccountDto account)
        {
            AddAccountValidator validator = new AddAccountValidator();
            var resultValidator = validator.Validate(account);

            if (!resultValidator.IsValid)
            {
                var errors = string.Join('\n', resultValidator.Errors);
                                
                return BadRequest(errors);
            }
                
            var result = await _accountService.AddAccount(account);
            return result is not null ? Created("", result) : BadRequest();
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IList<AccountResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccounts()
        {
            var result = await _accountService.GetAccounts();
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(IList<AccountResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccount([FromRoute] Guid id)
        {
            var result = await _accountService.GetAccount(id);
            return Ok(result);
        }
    }
}
