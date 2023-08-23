using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Application.Models.AtualizarDados;
using UsuariosApp.Tests.Helpers;
using Xunit;

namespace UsuariosApp.Tests.Facts
{
    /// <summary>
    /// Classe de testes para o endpoint de atualização de dados
    /// </summary>
    public class AtualizarDadosFact
    {
        [Fact]
        public void AtualizarDados_Returns_Ok()
        {
            var request = new AtualizarDadosRequestModel
            {
                Nome = "Usuário Teste",
                Senha = "@Teste123",
                SenhaConfirmacao = "@Teste123"
            };

            //autenticar na API e obtendo o token
            var accessToken = new AutenticarFact().Autenticar_Returns_Ok().AccessToken;

            //enviando o token no cabeçalho da requisição para a API
            var client = TestHelper.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            //realizando o cadastro na API
            var response = client.PutAsync("/api/usuarios/atualizar-dados",
                TestHelper.CreateContent(request)).Result;

            //verificando o resultado esperado X resultado obtido
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public void AtualizarDados_Returns_Unauthorized()
        {
            var request = new AtualizarDadosRequestModel
            {
                Nome = "Usuário Teste",
                Senha = "@Teste123",
                SenhaConfirmacao = "@Teste123"
            };

            //enviando o token no cabeçalho da requisição para a API
            //realizando o cadastro na API
            var response = TestHelper.CreateClient().PutAsync("/api/usuarios/atualizar-dados",
                TestHelper.CreateContent(request)).Result;

            //verificando o resultado esperado X resultado obtido
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }
    }
}



