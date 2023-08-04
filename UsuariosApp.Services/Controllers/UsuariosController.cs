using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsuariosApp.Services.Models.AtualizarDados;
using UsuariosApp.Services.Models.Autenticar;
using UsuariosApp.Services.Models.CriarConta;
using UsuariosApp.Services.Models.RecuperarSenha;

namespace UsuariosApp.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        [Route("criar-conta")]
        [HttpPost]
        [ProducesResponseType(typeof(CriarContaResponseModel), 201)]
        public IActionResult CriarConta([FromBody] CriarContaRequestModel model)
        {
            return Ok();
        }

        [Route("autenticar")]
        [HttpPost]
        [ProducesResponseType(typeof(AutenticarResponseModel), 200)]
        public IActionResult Autenticar([FromBody] AutenticarRequestModel model)
        {
            return Ok();
        }

        [Route("recuperar-senha")]
        [HttpPost]
        [ProducesResponseType(typeof(RecuperarSenhaResponseModel), 200)]
        public IActionResult RecuperarSenha([FromBody] RecuperarSenhaRequestModel model)
        {
            return Ok();
        }

        [Route("atualizar-dados")]
        [HttpPut]
        [ProducesResponseType(typeof(AtualizarDadosResponseModel), 200)]
        public IActionResult AtualizarDados([FromBody] AtualizarDadosRequestModel model)
        {
            return Ok();
        }
    }
}
