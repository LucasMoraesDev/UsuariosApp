using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Enums;
using UsuariosApp.Domain.Helpers;
using UsuariosApp.Domain.Interfaces.Messages;
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
        private readonly IUsuarioMessage? _usuarioMessage;

        public UsuarioDomainService(IUsuarioRepository? usuarioRepository, IHistoricoAtividadeRepository? historicoAtividadeRepository, ITokenSecurity? tokenSecurity, IUsuarioMessage? usuarioMessage)
        {
            _usuarioRepository = usuarioRepository;
            _historicoAtividadeRepository = historicoAtividadeRepository;
            _tokenSecurity = tokenSecurity;
            _usuarioMessage = usuarioMessage;
        }

        public void CriarConta(Usuario usuario)
        {
            //Regra: Verificar se o email do usuário já está cadastrado
            if (_usuarioRepository?.Get(usuario.Email) != null)
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
            if (usuario == null)
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
            //pesquisar o usuário no banco de dados através do email
            var usuario = _usuarioRepository?.Get(email);

            //verificar se o usuário não foi encontrado
            if (usuario == null)
            {
                throw new ApplicationException("Usuário não encontrado, verifique o email informado.");
            }

            //gerar uma nova senha para o usuário
            var novaSenha = PasswordHelper.GeneratePassword(true, true, true, true, 8);

            //atualizar a senha no banco de dados
            usuario.Senha = MD5Helper.Encrypt(novaSenha);
            _usuarioRepository?.Update(usuario);

            //enviar uma mensagem por email para o usuário com a nova senha
            var to = usuario.Email;
            var subject = "Recuperação de senha de acesso - COTI Informática";
            var body = $@"
                <div style='padding: 40px; margin: 40px; border: 1px solid #ccc; text-align: center;'>
                    <img src='https://www.cotiinformatica.com.br/imagens/logo-coti-informatica.png'/>
                    <hr/>
                    <h5>Olá {usuario.Nome}</h5>
                    <p>Uma nova senha de acesso foi gerada para você.</p>
                    <p>Acesse o sistema com a senha: {novaSenha}</p>
                    <br/>
                    <p>Att, equipe COTI Informática</p>
                </div>
            ";

            _usuarioMessage?.SendMessage(to, subject, body);

            //preenchendo o histórico desta atividade
            var historicoAtividade = new HistoricoAtividade
            {
                Id = Guid.NewGuid(),
                Tipo = TipoAtividade.RECUPERAÇÃO_DE_SENHA,
                DataHora = DateTime.Now,
                Descricao = $"Recuperação de senha do usuário {usuario.Nome} realizado com sucesso.",
                UsuarioId = usuario.Id
            };

            //gravando o histórico de atividade
            _historicoAtividadeRepository?.Create(historicoAtividade);

            return usuario;
        }

        public Usuario AtualizarDados(string? email, string nome, string senha)
        {
            //pesquisar o usuário no banco de dados através do email
            var usuario = _usuarioRepository?.Get(email);

            //verificar se o usuário não foi encontrado
            if (usuario == null)
            {
                throw new ApplicationException("Usuário não encontrado, verifique o email informado.");
            }

            var dadosAtualizados = false;

            //verificar se o nome está preenchido para edição
            if (!string.IsNullOrWhiteSpace(nome))
            {
                usuario.Nome = nome;
                dadosAtualizados = true;
            }

            //verificar se a senha está preenchida para edição
            if (!string.IsNullOrWhiteSpace(senha))
            {
                usuario.Senha = MD5Helper.Encrypt(senha);
                dadosAtualizados = true;
            }

            //verificar se há algum dado para ser atualizado
            //e então efetuar a edição no banco de dados
            if (dadosAtualizados)
            {
                _usuarioRepository?.Update(usuario);
            }
            else
            {
                throw new ApplicationException("Informe pelo menos 1 campo do usuário para ser atualizado.");
            }

            return usuario;
        }
    }
}



