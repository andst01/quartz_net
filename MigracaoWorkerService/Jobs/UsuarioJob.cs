﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MigracaoWorkerService.JobConfiguration;
using MigracaoWorkerService.Service.Models.Query;
using MigracaoWorkerService.Service.Repository.Query.Interface;
using Newtonsoft.Json;
using Quartz;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Jobs
{
    public class UsuarioJob : IJob
    {
        private readonly ILogger<UsuarioJob> _logger;
        private readonly IUsuarioQueryRepository _repositoryQuery;
        private readonly RabbitMqConfiguration _config;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IMapper mapper;


        public UsuarioJob(ILogger<UsuarioJob> logger,
                           IUsuarioQueryRepository repositoryQuery,
                           IOptions<RabbitMqConfiguration> config)
        {
            _logger = logger;
            _repositoryQuery = repositoryQuery;
            _config = config.Value;

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                        queue: "usuarioQueue",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

        }

        public Task Execute(IJobExecutionContext context)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);
                var message = JsonConvert.DeserializeObject<UsuarioQuery>(contentString);

                _repositoryQuery.Add(message);

                _channel.BasicAck(eventArgs.DeliveryTag, false);

                //var teste = JsonConvert.SerializeObject(message);
                _logger.LogInformation($"[{nameof(UsuarioJob)}].[log de informações de Estados] response : {JsonConvert.SerializeObject(message)}");
                _logger.LogInformation($"[{nameof(UsuarioJob)}].[log de informações de Estados] contadores data hora : {DateTime.Now.ToString()}");


            };

            _channel.BasicConsume("usuarioQueue", false, consumer);

            return Task.CompletedTask;

        }
    }
}
