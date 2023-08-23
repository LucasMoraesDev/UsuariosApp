using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Messages.Settings
{
    /// <summary>
    /// Parametros necessários para realizar envio de email
    /// </summary>
    public class EmailSettings
    {
        /// <summary>
        /// Conta de email utilizada para enviar as mensagens
        /// </summary>
        public string? Conta { get => "cotiaulajava@outlook.com"; }

        /// <summary>
        /// Senha da conta de email
        /// </summary>
        public string? Senha { get => "@Admin123456"; }

        /// <summary>
        /// Endereço do servidor SMTP da conta de email
        /// </summary>
        public string? Smtp { get => "smtp-mail.outlook.com"; }

        /// <summary>
        /// Porta do servidor para envio de emails
        /// </summary>
        public int Porta { get => 587; }
    }
}



