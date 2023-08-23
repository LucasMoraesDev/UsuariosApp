using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Application.Models.AtualizarDados;
using UsuariosApp.Application.Models.Autenticar;
using UsuariosApp.Application.Models.CriarConta;
using UsuariosApp.Application.Models.RecuperarSenha;

namespace UsuariosApp.Application.Interfaces
{
    public interface IUsuarioAppService
    {
        //método para a API criar uma conta de usuário
        CriarContaResponseModel CriarConta(CriarContaRequestModel model);

        //método para a API autenticar um usuário
        AutenticarResponseModel Autenticar(AutenticarRequestModel model);

        //método para a API recuperar senha de um usuário
        RecuperarSenhaResponseModel RecuperarSenha(RecuperarSenhaRequestModel model);

        //método para a API atualizar os dados de um usuário
        AtualizarDadosResponseModel AtualizarDados(AtualizarDadosRequestModel model, string email);
    }
}



