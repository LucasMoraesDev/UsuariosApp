using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Enums;
using UsuariosApp.Domain.Helpers;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Interfaces.Security;
using UsuariosApp.Domain.Interfaces.Services;

namespace UsuariosApp.Domain.Services
{
    public class UsuarioDomainService : IUsuarioDomainService
    {
        //atributos
        private readonly IUsuarioRepository? _usuarioRepository;
        private readonly IHistoricoAtividadeRepository? _historicoAtividadeRepository;
        private readonly ITokenSecurity? _tokenSecurity;

        public UsuarioDomainService(IUsuarioRepository? usuarioRepository, IHistoricoAtividadeRepository? historicoAtividadeRepository, ITokenSecurity? tokenSecurity)
        {
            _usuarioRepository = usuarioRepository;
            _historicoAtividadeRepository = historicoAtividadeRepository;
            _tokenSecurity = tokenSecurity;
        }

        public void CriarConta(Usuario usuario)
        {
            //Regra: Verificar se o email do usuário já está cadastrado
            if(_usuarioRepository?.Get(usuario.Email) != null)
            {
                throw new ApplicationException("O email informado já está cadastrado. Tente outro.");
            }

            //criptografar a senha do usuário
            usuario.Senha = MD5Helper.Encrypt(usuario.Senha);

            //cadastrar o usuário no banco de dados
            _usuarioRepository?.Create(usuario);

            //preenchendo o histórico desta atividade
            var historicoAtividade = new HistoricoAtividade
            {
                Id = Guid.NewGuid(),
                Tipo = TipoAtividade.CRIAÇÃO_DE_USUARIO,
                DataHora = DateTime.Now,
                Descricao = $"Cadastro do usuário {usuario.Nome} realizado com sucesso.",
                UsuarioId = usuario.Id
            };
            
             //cadastrar o historico desta atividade
            _historicoAtividadeRepository?.Create(historicoAtividade);
        }

        public Usuario Autenticar(string email, string senha)
        {
            //consultando o usuário no banco de dados através do email e da senha
            var usuario = _usuarioRepository?.Get(email, MD5Helper.Encrypt(senha));

            //verificando se o usuário não foi encontrado
            if(usuario == null)
            {
                throw new ApplicationException("Acesso negado. Usuário não encontrado.");
            }

            //preenchendo o histórico desta atividade
            var historicoAtividade = new HistoricoAtividade
            {
                Id = Guid.NewGuid(),
                Tipo = TipoAtividade.AUTENTICAÇÃO,
                DataHora = DateTime.Now,
                Descricao = $"Autenticação do usuário {usuario.Nome} realizado com sucesso.",
                UsuarioId = usuario.Id
            };

            //gravando o histórico de atividade
            _historicoAtividadeRepository?.Create(historicoAtividade);

            //gerando o token do usuário
            usuario.AccessToken = _tokenSecurity?.GenerateToken(usuario);

            //retornando os dados do usuário
            return usuario;
        }

        public Usuario RecuperarSenha(string email)
        {
            throw new NotImplementedException();
        }

        public bool AtualizarDados(Guid? id, string nome, string senha)
        {
            throw new NotImplementedException();
        }
    }
}
