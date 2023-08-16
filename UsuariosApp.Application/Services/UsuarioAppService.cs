using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Application.Interfaces;
using UsuariosApp.Application.Models.AtualizarDados;
using UsuariosApp.Application.Models.Autenticar;
using UsuariosApp.Application.Models.CriarConta;
using UsuariosApp.Application.Models.RecuperarSenha;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Interfaces.Services;

namespace UsuariosApp.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        //atributo
        private readonly IUsuarioDomainService? _usuarioDomainService;

        //construtor para injeção de dependência (inicialização dos atributos)
        public UsuarioAppService(IUsuarioDomainService? usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }

        public CriarContaResponseModel CriarConta(CriarContaRequestModel model)
        {
            //capturar os dados do usuário
            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = model.Nome,
                Email = model.Email,
                Senha = model.Senha,
                DataHoraCriacao = DateTime.Now.ToString("dd/MM/yyyy"),
                DataHoraUltimaAlteracao = DateTime.Now.ToString("dd/MM/yyyy")
            };

            //realizando o cadastro do usuário
            _usuarioDomainService?.CriarConta(usuario);

            //retornando a resposta
            var response = new CriarContaResponseModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataHoraCriacao = DateTime.Parse(usuario.DataHoraCriacao)
            };

            return response;
        }

        public AutenticarResponseModel Autenticar(AutenticarRequestModel model)
        {
            //realizando a autenticação do usuário na camada de domínio.
            var usuario = _usuarioDomainService?.Autenticar(model.Email, model.Senha);

            //retornar os dados do usuário autenticado
            var response = new AutenticarResponseModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                AccessToken = usuario.AccessToken,
                DataHoraAcesso = DateTime.Now,
                DataHoraExpiracao = DateTime.UtcNow.AddHours(2)
            };

            //retornando os dados da autenticação do usuário
            return response;
        }

        public RecuperarSenhaResponseModel RecuperarSenha(RecuperarSenhaRequestModel model)
        {
            throw new NotImplementedException();
        }

        public AtualizarDadosResponseModel AtualizarDados(AtualizarDadosRequestModel model)
        {
            throw new NotImplementedException();
        }
    }
}
