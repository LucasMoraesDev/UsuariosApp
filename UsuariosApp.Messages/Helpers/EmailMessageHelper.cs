using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Messages.Models;
using UsuariosApp.Messages.Settings;

namespace UsuariosApp.Messages.Helpers
{
    /// <summary>
    /// Classe auxiliar para envio de emails
    /// </summary>
    public class EmailMessageHelper
    {
        /// <summary>
        /// Método para realizar o envio do email
        /// </summary>
        public void SendMessage(UsuarioMessageModel model)
        {
            var settings = new EmailSettings();

            var mailMessage = new MailMessage(settings.Conta, model.To);
            mailMessage.Subject = model.Subject;
            mailMessage.Body = model.Body;
            mailMessage.IsBodyHtml = true;

            var smtpClient = new SmtpClient(settings.Smtp, settings.Porta);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(settings.Conta, settings.Senha);
            smtpClient.Send(mailMessage);
        }
    }
}



