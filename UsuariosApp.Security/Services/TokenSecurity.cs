using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Interfaces.Security;
using UsuariosApp.Security.Settings;

namespace UsuariosApp.Security.Services
{
    public class TokenSecurity : ITokenSecurity
    {
        public string GenerateToken(Usuario usuario)
        {
            //gerando a assinatura antifalsificação do token (VERIFY SIGNATURE)
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(TokenSettings.SecretKey);

            //criando o conteúdo do token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //nome do usuário autenticado (usaremos o email)
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, usuario.Email) }),
                //definindo a data de expiração do token
                Expires = DateTime.UtcNow.AddMinutes(TokenSettings.ExpirationInMinutes),
                //assinando o token (chave antifalsificação)
                SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            //retornando o token
            var accessToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(accessToken);
        }
    }
}


