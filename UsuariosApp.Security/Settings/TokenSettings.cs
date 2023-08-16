using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Security.Settings
{
    /// <summary>
    /// Classe para armazenar as configurações de geração do TOKEN
    /// </summary>
    public class TokenSettings
    {
        /// <summary>
        /// Chave secreta para assinatura antifalsificação do token
        /// </summary>
        public static string SecretKey { get => "48A96E0F16B842C08157B68DD1734522"; }

        /// <summary>
        /// Tempo de expiração do TOKEN
        /// </summary>
        public static int ExpirationInMinutes { get => 60; }
    }
}


