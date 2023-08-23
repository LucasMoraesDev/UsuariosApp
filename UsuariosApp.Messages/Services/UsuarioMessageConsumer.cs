using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Messages.Helpers;
using UsuariosApp.Messages.Models;
using UsuariosApp.Messages.Settings;

namespace UsuariosApp.Messages.Services
{
    /// <summary>
    /// Classe para ler e processar as mensagens gravadas na fila do RabbitMQ
    /// </summary>
    public class UsuarioMessageConsumer : BackgroundService
    {
        //atributos
        private readonly IServiceProvider? _serviceProvider;
        private IConnection? _connection;
        private IModel? _model;
        private RabbitMQSettings? _rabbitMQSettings;

        public UsuarioMessageConsumer(IServiceProvider? serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _rabbitMQSettings = new RabbitMQSettings();

            //conectando no servidor do RabbitMQ
            var connectionFactory = new ConnectionFactory { Uri = new Uri(_rabbitMQSettings.Host) };
            _connection = connectionFactory.CreateConnection();
            _model = _connection.CreateModel();

            //acessando a fila do RabbitMQ
            _model.QueueDeclare(
                queue: _rabbitMQSettings?.Queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //objeto utilizado para ler e processar a fila
            var consumer = new EventingBasicConsumer(_model);

            //Criando o mecanismo para ler cada item da fila
            consumer.Received += async (sender, args) =>
            {
                //processar o conteudo lido da fila
                //lendo e deserializando o conteudo do item obtido da fila
                var payload = Encoding.UTF8.GetString(args.Body.ToArray());
                var usuarioMessageModel = JsonConvert.DeserializeObject<UsuarioMessageModel>(payload);

                //enviando o email para o usuário
                using (var scope = _serviceProvider.CreateScope())
                {
                    //disparando o email para o usuário
                    var emailMessageHelper = new EmailMessageHelper();
                    emailMessageHelper.SendMessage(usuarioMessageModel);

                    //removendo o item da fila
                    _model.BasicAck(args.DeliveryTag, false);
                }
            };

            //processar e retirar o item lido da fila
            _model.BasicConsume(_rabbitMQSettings?.Queue, false, consumer);
        }
    }
}



