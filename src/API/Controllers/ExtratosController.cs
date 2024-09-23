using Business.Contracheque.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExtratosController : ControllerBase
    {

        private readonly IContrachequeService _contrachequeService;

        public ExtratosController(IContrachequeService contrachequeService)
        {
            _contrachequeService = contrachequeService;
        }

        /// <summary>
        ///  Retorna o extrato mensal do funcionário
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Model Resultado {Erro, Mensagem, Data}</returns>
        [HttpGet("{id}")]
        public IActionResult ExtratoMensal([FromRoute] int id)
        {
            var resultado = _contrachequeService.ExtratoMensal(id);

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
    }
}
