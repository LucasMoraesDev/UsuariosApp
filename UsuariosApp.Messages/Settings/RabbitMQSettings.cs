using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Messages.Settings
{
    /// <summary>
    /// Classe para definir os parametros de conexão
    /// do servidor de mensageria do RabbitMQ
    /// </summary>
    public class RabbitMQSettings
    {
        /// <summary>
        /// Endereço do servidor do RabbitMQ (cloudamqp)
        /// </summary>
        public string Host { get => "amqps://vtrzgefp:tZcLgZNxwnSpLKDvVwoYXHCoVHd5J-vs@jackal.rmq.cloudamqp.com/vtrzgefp"; }

        /// <summary>
        /// Nome da fila que será criada/acessada
        /// </summary>
        public string Queue { get => "mensagens_usuarios"; }
    }
}



