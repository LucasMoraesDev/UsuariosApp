using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsuariosApp.Application.Interfaces;
using UsuariosApp.Application.Models.AtualizarDados;
using UsuariosApp.Application.Models.Autenticar;
using UsuariosApp.Application.Models.CriarConta;
using UsuariosApp.Application.Models.RecuperarSenha;

namespace UsuariosApp.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        //atributo
        private readonly IUsuarioAppService? _usuarioAppService;

        //método construtor para injeção de dependência (inicialização dos atributos)
        public UsuariosController(IUsuarioAppService? usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        [Route("criar-conta")]
        [HttpPost]
        [ProducesResponseType(typeof(CriarContaResponseModel), 201)]
        public IActionResult CriarConta([FromBody] CriarContaRequestModel model)
        {
            try
            {
                //executando o cadastro na camada de aplicação
                var response = _usuarioAppService?.CriarConta(model);

                //HTTP 201 (CREATED)
                return StatusCode(201, response);
            }
            catch (ApplicationException e)
            {
                //HTTP BAD REQUEST (400)
                return StatusCode(400, new { e.Message });
            }
            catch (Exception e)
            {
                //HTTP INTERNAL SERVER ERROR (500)
                return StatusCode(500, new { e.Message });
            }
        }

        [Route("autenticar")]
        [HttpPost]
        [ProducesResponseType(typeof(AutenticarResponseModel), 200)]
        public IActionResult Autenticar([FromBody] AutenticarRequestModel model)
        {
            try
            {
                //executando a autenticação do usuário na camada de aplicação
                var response = _usuarioAppService?.Autenticar(model);

                //HTTP 200 (OK)
                return StatusCode(200, response);
            }
            catch (ApplicationException e)
            {
                //HTTP UNAUTHORIZED (401)
                return StatusCode(401, new { e.Message });
            }
            catch (Exception e)
            {
                //HTTP BAD REQUEST (500)
                return StatusCode(500, new { e.Message });
            }
        }

        [Route("recuperar-senha")]
        [HttpPost]
        [ProducesResponseType(typeof(RecuperarSenhaResponseModel), 200)]
        public IActionResult RecuperarSenha([FromBody] RecuperarSenhaRequestModel model)
        {
            try
            {
                //executando a recuperação da senha do usuário
                var response = _usuarioAppService?.RecuperarSenha(model);

                //HTTP 200 (OK)
                return StatusCode(200, response);
            }
            catch (ApplicationException e)
            {
                //HTTP BAD REQUEST (400)
                return StatusCode(400, new { e.Message });
            }
            catch (Exception e)
            {
                //HTTP BAD REQUEST (500)
                return StatusCode(500, new { e.Message });
            }
        }

        [Authorize]
        [Route("atualizar-dados")]
        [HttpPut]
        [ProducesResponseType(typeof(AtualizarDadosResponseModel), 200)]
        public IActionResult AtualizarDados([FromBody] AtualizarDadosRequestModel model)
        {
            try
            {
                //capturar o email do usuário autenticado, através do TOKEN
                var email = User.Identity.Name;

                //atualizar os dados do usuário
                var response = _usuarioAppService?.AtualizarDados(model, email);

                //HTTP 200 (OK)
                return StatusCode(200, response);
            }
            catch (ApplicationException e)
            {
                //HTTP BAD REQUEST (400)
                return StatusCode(400, new { e.Message });
            }
            catch (Exception e)
            {
                //HTTP BAD REQUEST (500)
                return StatusCode(500, new { e.Message });
            }
        }
    }
}


