using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Application.Models.CriarConta;
using UsuariosApp.Tests.Helpers;
using Xunit;

namespace UsuariosApp.Tests.Facts
{
    /// <summary>
    /// Classe de testes para o endpoint de criação de conta de usuários
    /// </summary>
    public class CriarContaFact
    {
        [Fact]
        public CriarContaRequestModel CriarConta_Returns_Ok()
        {
            var faker = new Faker("pt_BR");

            //criando os dados para realizar o cadastro do usuário
            var request = new CriarContaRequestModel
            {
                Nome = faker.Person.FullName,
                Email = faker.Internet.Email(),
                Senha = "@Teste123",
                SenhaConfirmacao = "@Teste123"
            };

            //executando uma requisição POST para cadastrar o usuário
            var response = TestHelper.CreateClient().PostAsync("/api/usuarios/criar-conta",
                TestHelper.CreateContent(request)).Result;

            //comparar o resultado obtido com o resultado esperado
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            //retornar os dados do usuário cadastrado
            return request;
        }

        [Fact]
        public void CriarConta_Returns_BadRequest()
        {
            //executando o teste para cadastro do usuário
            //e capturando os dados que foram cadastrados
            var request = CriarConta_Returns_Ok();

            //executando uma requisição POST para cadastrar o usuário
            var response = TestHelper.CreateClient().PostAsync("/api/usuarios/criar-conta",
                TestHelper.CreateContent(request)).Result;

            //comparar o resultado obtido com o resultado esperado
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}



