using CashFlow.Application.Dtos;
using CashFlow.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<UserReponseDto>),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync();
            return Ok(result.ToList());
        }
    }
}
