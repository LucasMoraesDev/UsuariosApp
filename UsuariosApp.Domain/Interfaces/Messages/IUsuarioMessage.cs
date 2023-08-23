using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Interfaces.Messages
{
    public interface IUsuarioMessage
    {
        void SendMessage(string to, string subject, string body);
    }
}



