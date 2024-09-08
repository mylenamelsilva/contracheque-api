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

        /// <summary>
        ///  Cria um novo funcionário na base de dados
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Model Resultado {Erro, Mensagem, Data}</returns>
        [HttpPost]
        public IActionResult CriarFuncionario([FromBody] UserRequestDto dto)
        {
            var resultado = _userService.CriarFuncionario(dto);

            if (resultado.Erro)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        /// <summary>
        ///  Busca um funcionário na base de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Model Resultado {Erro, Mensagem, Data}</returns>
        [HttpGet("{id}")]
        public IActionResult MostrarFuncionario([FromRoute] int id)
        {
            var resultado = _userService.MostrarFuncionario(id);

            if (resultado.Erro)
            {
                return BadRequest(resultado);
            }

            if (resultado.Data == null)
            {
                return NotFound();
            }

            return Ok(resultado);
        }

        /// <summary>
        ///  Altera um funcionário na base de dados
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Model Resultado {Erro, Mensagem, Data}</returns>
        [HttpPut("{id}")]
        public IActionResult AtualizarInformacoesFuncionario([FromRoute] int id, [FromBody] AtualizarInformacoesRequestDto dto)
        {
            var resultado = _userService.AtualizarFuncionario(id, dto);

            if (resultado.Erro)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        /// <summary>
        ///  Remove funcionário na base de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Model Resultado {Erro, Mensagem, Data}</returns>
        [HttpDelete("{id}")]
        public IActionResult RemoverFuncionario([FromRoute] int id)
        {
            var resultado = _userService.RemoverFuncionario(id);

            if (resultado.Erro)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }
    }
}
