using Bogus;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Application.Models.RecuperarSenha;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Tests.Helpers;
using Xunit;

namespace UsuariosApp.Tests.Facts
{
    /// <summary>
    /// Classe de testes para o endpoint de recuperação de senha de usuários
    /// </summary>
    public class RecuperarSenhaFact
    {
        [Fact]
        public void RecuperarSenha_Returns_Ok()
        {
            //criando um usuário no sistema..
            var criarContaFact = new CriarContaFact();
            var usuario = criarContaFact.CriarConta_Returns_Ok();

            //dados que serão enviados para a recuperação da senha
            var request = new RecuperarSenhaRequestModel
            {
                Email = usuario.Email,
            };

            //executando uma requisição POST para cadastrar o usuário
            var response = TestHelper.CreateClient().PostAsync("/api/usuarios/recuperar-senha",
                TestHelper.CreateContent(request)).Result;

            //comparar o resultado obtido com o resultado esperado
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public void RecuperarSenha_Returns_BadRequest()
        {
            var faker = new Faker("pt_BR");

            //dados que serão enviados para a recuperação da senha
            var request = new RecuperarSenhaRequestModel
            {
                Email = faker.Internet.Email(),
            };

            //executando uma requisição POST para cadastrar o usuário
            var response = TestHelper.CreateClient().PostAsync("/api/usuarios/recuperar-senha",
                TestHelper.CreateContent(request)).Result;

            //comparar o resultado obtido com o resultado esperado
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}



