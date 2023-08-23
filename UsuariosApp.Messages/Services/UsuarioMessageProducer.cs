using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Interfaces.Messages;
using UsuariosApp.Messages.Models;
using UsuariosApp.Messages.Settings;

namespace UsuariosApp.Messages.Services
{
    /// <summary>
    /// Classe para acessar e escrever mensagens na fila do RabbitMQ
    /// </summary>
    public class UsuarioMessageProducer : IUsuarioMessage
    {
        public void SendMessage(string to, string subject, string body)
        {
            //criar um objeto com os dados que serão gravados na fila
            var model = new UsuarioMessageModel
            {
                Id = Guid.NewGuid(),
                To = to,
                Subject = subject,
                Body = body,
                CreatedAt = DateTime.Now,
            };

            //instando a classe Settings
            var settings = new RabbitMQSettings();

            //configurando o acesso ao servidor do RabbitMQ
            var connectionFactory = new ConnectionFactory
            {
                //caminho do servidor da mensageria criado no cloudamqp
                Uri = new Uri(settings.Host)
            };

            //realizar a conexão com o servidor
            using (var connection = connectionFactory.CreateConnection())
            {
                //criando a fila..
                using (var queue = connection.CreateModel())
                {
                    //se a fila não existir, iremos cria-la
                    queue.QueueDeclare(
                        queue: settings.Queue, //nome da fila
                        durable: true, //não apagar as filas ao desligar ou reiniciar o broker
                        autoDelete: false, //apagar ou não a fila quando ela estiver sem mensagens (vazia)
                        exclusive: false, //fila exclusiva para uma unica aplicaçao ou não
                        arguments: null
                    );

                    //escrevendo conteudo na fila
                    queue.BasicPublish(
                        exchange: string.Empty,
                        routingKey: settings.Queue,
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model))
                        );
                }
            }
        }
    }
}



