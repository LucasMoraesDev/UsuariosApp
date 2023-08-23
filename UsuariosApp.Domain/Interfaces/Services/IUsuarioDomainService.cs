using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Domain.Interfaces.Services
{
    public interface IUsuarioDomainService
    {
        //método para criação da conta do usuário
        void CriarConta(Usuario usuario);

        //método para autenticar o usuário
        Usuario Autenticar(string email, string senha);

        //método para recuperação da senha do usuário
        Usuario RecuperarSenha(string email);

        //método para atualizar os dados do usuário
        Usuario AtualizarDados(string? email, string nome, string senha);
    }
}



