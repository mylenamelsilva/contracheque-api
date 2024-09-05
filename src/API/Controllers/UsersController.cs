using Business.Users;
using Business.Users.Interfaces;
using Business.Users.Records;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult CriarFuncionario(UserRequestDto dto)
        {
            var resultado = _userService.CriarFuncionario(dto);

            if (resultado.Erro)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

    }
}
