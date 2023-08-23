using Azure;
using Bogus;
using Bogus.DataSets;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Application.Models.Autenticar;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Tests.Helpers;
using Xunit;

namespace UsuariosApp.Tests.Facts
{
    /// <summary>
    /// Classe de testes para o endpoint de autenticação de usuários
    /// </summary>
    public class AutenticarFact
    {
        [Fact]
        public AutenticarResponseModel Autenticar_Returns_Ok()
        {
            //cadastrando um usuário
            var criarContaFact = new CriarContaFact();
            var usuario = criarContaFact.CriarConta_Returns_Ok();

            //definindo os dados da requisição
            var request = new AutenticarRequestModel
            {
                Email = usuario.Email,
                Senha = usuario.Senha
            };

            //realizando o cadastro na API
            var response = TestHelper.CreateClient().PostAsync("/api/usuarios/autenticar",
                TestHelper.CreateContent(request)).Result;

            //verificando o resultado esperado X resultado obtido
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            //capturando o retorno da API
            return JsonConvert.DeserializeObject<AutenticarResponseModel>
                (response.Content.ReadAsStringAsync().Result);
        }

        [Fact]
        public void Autenticar_Returns_Unauthorized()
        {
            var faker = new Faker("pt_BR");

            //definindo os dados da requisição
            var request = new AutenticarRequestModel
            {
                Email = faker.Internet.Email(),
                Senha = "@Teste1234"
            };

            //realizando o cadastro na API
            var response = TestHelper.CreateClient().PostAsync("/api/usuarios/autenticar",
                TestHelper.CreateContent(request)).Result;

            //verificando o resultado esperado X resultado obtido
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void Autenticar_Returns_BadRequest()
        {
            //definindo os dados da requisição
            var request = new AutenticarRequestModel
            {
                Email = string.Empty,
                Senha = string.Empty
            };

            //realizando o cadastro na API
            var response = TestHelper.CreateClient().PostAsync("/api/usuarios/autenticar",
                TestHelper.CreateContent(request)).Result;

            //verificando o resultado esperado X resultado obtido
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}



